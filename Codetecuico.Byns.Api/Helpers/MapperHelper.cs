using Codetecuico.Byns.Api.Models;
using AutoMapper;
using System.Collections.Generic;
using Codetecuico.Byns.Domain;

namespace Codetecuico.Byns.Api.Helpers
{
    public static class MapperHelper
    {
        internal static UserModel Map(User user)
        {
            return Mapper.Map<User, UserModel>(user);
        }

        internal static User Map(UserModel user)
        {
            return Mapper.Map<UserModel, User>(user);
        }

        internal static IEnumerable<ItemModel> Map(IEnumerable<Item> items)
        {
            return Mapper.Map<IEnumerable<Item>, IEnumerable<ItemModel>>(items);
        }

        internal static Item Map(ItemModel item)
        {
            return Mapper.Map<ItemModel, Item>(item);
        }

        internal static ItemModel Map(Item item)
        {
            return Mapper.Map<Item, ItemModel>(item);
        }

        internal static Item Map(ItemModel itemModel, Item item)
        {
            return Mapper.Map(itemModel, item);
        }

        internal static User Map(UserModel userModel, User user)
        {
            return Mapper.Map(userModel, user);
        }

        internal static IEnumerable<UserModel> Map(IEnumerable<User> users)
        {
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
        }
    }
}