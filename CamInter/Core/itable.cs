﻿using System;
using System.Data;
using System.Collections.Generic;

namespace Core
{
    public interface itable
    {
        /// <summary>
        /// 数据表信息解码
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        ValueType DecodeOneItemByDb(DataRow dr);
        /// <summary>
        /// 获取所有信息列表
        /// </summary>
        /// <returns></returns>
        List<ValueType> GetAllData();
        /// <summary>
        /// 表结构
        /// </summary>
        /// <returns></returns>
        string CreateTableStructure();
        //void EncodeOneItemToDB(
        /// <summary>
        /// 插入一条新记录
        /// </summary>
        /// <param name="item"></param>
        bool InsertOneItem(ValueType item);
    }

    public interface iGetIDName
    {
        int Idx { get; }
        string Name { get; }
        bool IsShowInList { get; }
    }
}
