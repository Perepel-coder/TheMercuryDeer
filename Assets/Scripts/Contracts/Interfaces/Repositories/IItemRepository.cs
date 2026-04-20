using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Application.Interfaces.Repositories
{
    public interface IItemRepository
    {
        UniTask<List<ItemDTO>> GetItemsAsync();
        List<ItemDTO> GetItems();

        UniTask<ItemDTO> GetItemAsync(int id);
        ItemDTO GetItem(int id);

        UniTask<ItemDTO> GetItemByTagAsync(Tag tag);
        ItemDTO GetItemByTag(Tag tag);


        UniTask<List<ItemDTO>> GetItemsByCategoryAsync(Category category);
        List<ItemDTO> GetItemsByCategory(Category category);

        UniTask<int> AddItemAsync(ItemDTO item);
        int AddItem(ItemDTO item);

        UniTask UpdateItemAsync(ItemDTO item);
        void UpdateItem(ItemDTO item);

        UniTask<int> DeleteItemAsync(ItemDTO item);
        int DeleteItem(ItemDTO item);
    }
}
