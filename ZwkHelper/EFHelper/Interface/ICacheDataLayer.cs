using System;
using System.Collections.Generic;
using System.Text;
using CacheHelper.Interface;
using EFHelper.Interface.Item;
using EFHelper.Model;
using Microsoft.EntityFrameworkCore;

namespace EFHelper.Interface
{
    public interface ICacheDataLayer<T> : IDataLayer<T>, ICacheSelect<T> where T : BaseModel
    {
        ICacheService CacheService { get; set; }

    }

}
