using Assets.Scripts.Application.Interfaces.Repositories;
using Assets.Scripts.Application.Mappers;
using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Application.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ISQLiteAsyncConnection _asyncConnection;

        public ItemRepository(ISQLiteAsyncConnection asyncConnection) => _asyncConnection = asyncConnection;

        public async UniTask<List<ItemDTO>> GetItemsAsync()
        {
            var items = await _asyncConnection.Table<Item>().ToListAsync();
            var categoriesLookup = (await _asyncConnection.Table<ItemCategory>().ToListAsync())
                .ToLookup(icc => icc.ItemId, icc => icc.Category);

            return items .Select(item => item.ToDTO(categoriesLookup[item.Id])).ToList();
        }

        public async UniTask<ItemDTO> GetItemAsync(int id) => (await GetItemsAsync()).FirstOrDefault(i => i.Id == id);

        public async UniTask<ItemDTO> GetItemByTagAsync(Tag tag) => (await GetItemsAsync()).FirstOrDefault(i => i.ItemTag == tag);

        public async UniTask<List<ItemDTO>> GetItemsByCategoryAsync(Category category) => (await GetItemsAsync()).Where(i => i.Categories.Contains(category)).ToList();

        public async UniTask<int> AddItemAsync(ItemDTO item)
        {
            if (await _asyncConnection.FindAsync<Item>(item.Id) != null)
                return item.Id;

            Item itemModel = item.ToModel();

            await _asyncConnection.InsertAsync(itemModel);

            foreach (var category in item.Categories)
            {
                await _asyncConnection.InsertAsync(new ItemCategory
                {
                    ItemId = itemModel.Id,
                    Category = category
                });
            }

            return itemModel.Id;
        }

        public async UniTask UpdateItemAsync(ItemDTO item)
        {
            if (await _asyncConnection.FindAsync<Item>(item.Id) == null)
                return;

            await _asyncConnection.UpdateAsync(item.ToModel());

            await _asyncConnection.Table<ItemCategory>()
                .DeleteAsync(ic => ic.ItemId == item.Id);

            foreach (var category in item.Categories)
            {
                await _asyncConnection.InsertAsync(new ItemCategory
                {
                    ItemId = item.Id,
                    Category = category
                });
            }
        }

        public async UniTask<int> DeleteItemAsync(ItemDTO item) => await _asyncConnection.DeleteAsync(item.ToModel());



        public List<ItemDTO> GetItems() => Task.Run(() => GetItemsAsync().AsTask()).GetAwaiter().GetResult();

        public ItemDTO GetItem(int id) => Task.Run(() => GetItemAsync(id).AsTask()).GetAwaiter().GetResult();

        public ItemDTO GetItemByTag(Tag tag) => Task.Run(() => GetItemByTagAsync(tag).AsTask()).GetAwaiter().GetResult();

        public List<ItemDTO> GetItemsByCategory(Category category) => Task.Run(() => GetItemsByCategoryAsync(category).AsTask()).GetAwaiter().GetResult();

        public int AddItem(ItemDTO item) => Task.Run(() => AddItemAsync(item).AsTask()).GetAwaiter().GetResult();

        public void UpdateItem(ItemDTO item) => Task.Run(() => UpdateItemAsync(item).AsTask()).GetAwaiter().GetResult();

        public int DeleteItem(ItemDTO item) => Task.Run(() => DeleteItemAsync(item).AsTask()).GetAwaiter().GetResult();
    }
}
