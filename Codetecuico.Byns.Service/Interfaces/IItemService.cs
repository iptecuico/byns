using Codetecuico.Byns.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codetecuico.Byns.Service
{
    public interface IItemService : IService<Item>
    {
        bool Delete(int id);
    }
}
