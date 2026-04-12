# 🦌 The Mercury Deer

2D экшн-игра на Unity, в которой игрок сражается с врагами, обладающими поведенческим ИИ на основе состояний.

---

## 🎮 Геймплей

- Передвижение игрока по 2D-миру
- Рывок (Dash) с кулдауном
- Атаки мечом с прицеливанием через положение мыши
- Полоска здоровья игрока в HUD
- Враги самостоятельно патрулируют, преследуют и атакуют
- Вспышка белым цветом на спрайте при получении урона
- Эффекты разрушения объектов окружения
- Всплывающие цифры урона над целью
- Инвентарь: подбор предметов, использование расходников, выброс предметов

---

## 🚀 Быстрый старт

### Требования

- **Unity 6** (6000.3.10f1 или новее) — скачать через [Unity Hub](https://unity.com/download)
- Git

### Установка

```bash
git clone https://github.com/Perepel-coder/TheMercuryDeer.git
```

1. Открыть **Unity Hub** → *Add project from disk* → выбрать склонированную папку.
2. Дождаться импорта ассетов (первый запуск может занять несколько минут).
3. Открыть сцену `Assets/Scenes/0.unity`.
4. Нажать **Play**.

> База данных SQLite (`game.db`) создаётся автоматически при первом запуске в `Application.persistentDataPath`. Ручная настройка не требуется.

### Управление

| Кнопка | Действие |
|--------|----------|
| `WASD` | Движение |
| `Dash` (настраивается в Input Actions) | Рывок |
| ЛКМ | Атака |
| Мышь | Направление атаки |
| `E` | Подобрать предмет |
| `I` / кнопка инвентаря | Открыть / закрыть инвентарь |

---

## 🏗️ Архитектура

Проект построен на **многоуровневой чистой архитектуре**:

```
Assets/Scripts/
├── Application/
│   ├── Interfaces/       # IHasHealth, IDamageable, IHealable, IStateHandler, IRepository …
│   ├── Mappers/          # PlayerMapper, EnemyMapper, WeaponMapper
│   └── Repositories/     # PlayerRepository, EnemyRepository, WeaponRepository, ItemRepository
├── Infrastructure/
│   └── DatabaseService   # SQLite-инициализация; предоставляет типизированные репозитории
├── Models/               # SQLite-net модели таблиц (Player, Enemy, Weapon, Item, ItemCategory)
├── DTO/                  # Runtime объекты передачи данных
├── Services/
│   ├── PlayerServices/        # PlayerService, PlayerEntityService, PlayerViewService
│   ├── EnemyServices/         # BaseEnemyAIService, обработчики состояний, конкретные сервисы врагов
│   ├── WeaponServices/        # ActiveWeaponService, PlayerSwordService, AmorSwordService …
│   ├── InventorySystemServices/
│   │   ├── UI/                # InventoryManagerService, InventorySlotsPanel, InventorySlot, InventoryDescriptionPanel
│   │   └── ItemServices/      # BaseItemService, ConsumableService, WeaponService
│   ├── UIServices/            # GameMainCanvasService, PopUpDamageService, PopUpHintService
│   └── VisualEffectServices/  # FlashBlinkService, BaseDestructibleObjectService, DestructionHandlerService
├── Enums/                     # EnemyDefinitions, ItemDefinitions (Category, Tag, StatToChange)
└── Paths/                     # AnimatorParameters, AnimationPaths, ResourcePaths (строковые константы)
```

### Постоянное хранение данных

Характеристики игрока, врагов и оружия (скорость, тайминг рывка, дальность атаки, урон и т.д.) хранятся в **SQLite**-базе данных через `sqlite-net-pcl`.  
Таблицы создаются при первом запуске через `DatabaseService.Initialize()`, вызываемый до загрузки любой сцены (`RuntimeInitializeOnLoadMethod`).

---

## 🤖 Система ИИ врагов

Каждый враг работает через **конечный автомат (FSM)**:

```
Roaming → Chasing → Attacking
   ↑                    ↓
   └────────────────────┘
```

| Состояние | Описание |
|-----------|----------|
| `Roaming` | Случайное патрулирование в заданном радиусе |
| `Chasing` | Преследование игрока при обнаружении в радиусе `ChasingDistance` |
| `Attacking` | Атака при сближении на `AttackingDistance`, с кулдауном `AttackRate` |

Навигация реализована через **NavMeshPlus** (2D NavMesh).

---

## ⚔️ Враги

| Враг | Оружие | Особенности |
|------|--------|-------------|
| **Amor** | `AmorSword` | Не меняет направление взгляда во время атаки |

Все числовые параметры (HP, скорость, урон, дистанция) настраиваются через таблицу `Enemy` в базе данных.

---

## 🗡️ Система оружия

| Тег | Носитель | Механика |
|-----|----------|----------|
| `PlayerSword` | Игрок | Удар с хит-боксом; направление — к курсору мыши |
| `AmorSword` | Amor | Падающий удар; блокирует поворот носителя во время анимации |
| `BaseReactionToTakingHit` | — | Базовая реакция на получение урона |

**`ActiveWeaponService`** — компонент, управляющий активным оружием персонажа и его поворотом к цели.

---

## 🎒 Система инвентаря

Инвентарь открывается и закрывается горячей клавишей. При открытии игра ставится на паузу (`Time.timeScale = 0`), боевой ввод отключается.

### UI-компоненты

| Компонент | Описание |
|-----------|----------|
| `InventoryManagerService` | Центральный контроллер: открытие/закрытие окна, добавление предметов в слоты |
| `InventorySlotsPanel` | Контейнер слотов; поиск пустого слота или слота с тем же `ItemTag` (стекинг) |
| `InventorySlot` | Один слот: иконка, счётчик количества (макс. **10**); ЛКМ — открыть описание, ПКМ — выбросить все предметы из слота на сцену |
| `InventoryDescriptionPanel` | Панель справа: иконка, название, описание предмета и кнопка **«Использовать»** |

### Предметы на сцене

| Класс | Описание |
|-------|----------|
| `BaseItemService` | Абстрактный базовый класс предмета в мире: зона взаимодействия (`1 unit`), всплывающая подсказка `«е»`, подбор по кнопке `E` (`OnPlayerInteractWithItem`) |
| `ConsumableService` | Расходуемый предмет (пример: **RedApple**): при использовании восстанавливает процент от максимального HP игрока |
| `WeaponService` | Предмет-оружие в мире |

### Категории и теги предметов

```
Category   : Weapon | Armor | Consumable | CraftingMaterial
Tag        : AmorSword | PlayerSword | BaseReactionToTakingHit | RedApple | None
StatToChange: Health
```

### Данные предметов

Параметры каждого предмета (название, описание, изменяемая характеристика, процент изменения, категории) хранятся в таблице `Item` базы данных SQLite и загружаются через `ItemRepository.GetItemByTag(tag)`.

---

## ✨ Визуальные эффекты

| Сервис | Эффект |
|--------|--------|
| `FlashBlinkService` | Белая вспышка на спрайте при попадании |
| `DestructionHandlerService` | Запуск `ParticleSystem` при разрушении объекта |
| `BaseDestructibleObjectService` | Базовая логика разрушаемых объектов окружения |
| `PopUpDamageService` | Всплывающая цифра урона с физикой (Rigidbody2D) |

---

## 🛠️ Технологии

| Технология | Версия | Назначение |
|------------|--------|------------|
| **Unity** | 6000.3.10f1 | Игровой движок |
| **URP 2D** | 17.3.0 | 2D-рендеринг |
| **C# / .NET Standard** | 2.1 | Игровая логика |
| **UniTask** | latest | Асинхронные операции без аллокаций |
| **Unity Input System** | 1.18.0 | Ввод с клавиатуры и мыши |
| **Unity AI Navigation** | 2.0.11 | NavMesh-навигация |
| **NavMeshPlus** | — | 2D-расширение NavMesh |
| **SuperTiled2Unity** | — | Импорт карт из Tiled |
| **sqlite-net-pcl** | 1.9.172 | SQLite ORM |
| **NuGet for Unity** | — | Управление NuGet-пакетами в Unity |

---

## 📁 Структура проекта

```
The Mercury Deer/
├── Assets/
│   ├── InputActions/        # GameInput и InputActions
│   ├── Resources/
│   │   └── Sprites/         # Спрайт-листы (Amor, Player, Environment)
│   ├── Scenes/              # Сцены игры (0.unity …)
│   ├── Scripts/             # Вся игровая логика (см. Архитектуру выше)
│   └── Packages/            # Вендорные NuGet-пакеты (sqlite-net-pcl …)
├── Packages/                # Unity Package Manager (manifest.json)
└── ProjectSettings/         # Настройки Unity-проекта
```

---

## 📌 Статус проекта

Проект находится в активной разработке.

| Система | Статус |
|---------|--------|
| Движение и рывок игрока | ✅ Готово |
| Атака мечом | ✅ Готово |
| ИИ врагов (FSM) | ✅ Готово |
| Визуальные эффекты | ✅ Готово |
| Система оружия | ✅ Готово |
| Система инвентаря | ✅ Готово |