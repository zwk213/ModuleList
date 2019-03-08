using System;
using System.Collections.Generic;
using System.Text;
using EFHelper.Interface.Item;
using EFHelper.Model;

namespace EFHelper.Interface
{
    public interface ICacheDataLayer<T> : IDataLayer<T>, ICacheSelect<T> where T : BaseModel
    {

    }

}
