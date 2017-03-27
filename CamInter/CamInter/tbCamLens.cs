﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SQLite;

namespace CamInter
{
    
    /// <summary>
    /// 镜头属性
    /// </summary>
    class tbCamLens : itable
    {
        private string STR_CONNECTION;
        public tbCamLens(string strConn)
        {
            this.STR_CONNECTION = strConn;
        }

        public ValueType DecodeOneItemByDb(DataRow dr)
        {
            CameraLens info = new CameraLens();

            info.Idx = Convert.ToInt32(dr["id"]);
            info.Name = dr["name"].ToString ();
            info.Number = dr["number"].ToString();
            info.Sensor = Convert.ToInt32(dr["sensor"]);
            info.Fov = Convert.ToInt32(dr["fov"]);
            info.Focus = Convert.ToSingle(dr["focus"]);
            info.Flange = Convert.ToSingle(dr["flange"]);
            info.Connector = Convert.ToInt32(dr["connector"]);
            info.Ratio = Convert.ToSingle(dr["ratio"]);
            info.RatioMin = Convert.ToSingle(dr["ratioMin"]);
            info.RatioMax = Convert.ToSingle(dr["ratioMax"]);
            info.Weight = Convert.ToInt32(dr["weight"]);
            info.Contrast = Convert.ToInt32(dr["contrast"]);
            info.Target = Convert.ToSingle(dr["target"]);
            info.Distort = Convert.ToInt32(dr["distort"]);

            return info;
        }

        public string CreateTableStructure()
        {
            return @"create table camLens(
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name nvarchar(20),
                        number nvarchar(20),
                        sensor int,
                        fov int,
                        connector int,
                        focus float,
                        flange float,
                        ratio float,
                        ratioMin float,
                        ratioMax float,
                        target float,
                        weight int,
                        contrast int,
                        distort int);";
        }

        public bool InsertOneItem(ValueType item)
        {
            CameraLens info = (CameraLens)item;

            string strSql = @"insert into camLens (name,number,sensor,fov,connector,focus,flange,ratio,ratioMin,ratioMax,target,weight,contrast,distort) 
                                   values (@name,@number,@sensor,@fov,@connector,@focus,@flange,@ratio,@ratioMin,@ratioMax,@target,@weight,@contrast,@distort)";
            List<SQLiteParameter> paraList = new List<SQLiteParameter>();
            paraList.Add(SQLiteHelper.CreateParameter("@name", DbType.String, info.Name));
            paraList.Add(SQLiteHelper.CreateParameter("@number", DbType.String, info.Number));
            paraList.Add(SQLiteHelper.CreateParameter("@sensor", DbType.Int32, info.Sensor));
            paraList.Add(SQLiteHelper.CreateParameter("@fov", DbType.Int32, info.Fov));
            paraList.Add(SQLiteHelper.CreateParameter("@connector", DbType.Int32, info.Connector));
            paraList.Add(SQLiteHelper.CreateParameter("@focus", DbType.Single, info.Focus));
            paraList.Add(SQLiteHelper.CreateParameter("@flange", DbType.Single, info.Flange));
            paraList.Add(SQLiteHelper.CreateParameter("@ratio", DbType.Single, info.Ratio));
            paraList.Add(SQLiteHelper.CreateParameter("@ratioMin", DbType.Single, info.RatioMin));
            paraList.Add(SQLiteHelper.CreateParameter("@ratioMax", DbType.Single, info.RatioMax));
            paraList.Add(SQLiteHelper.CreateParameter("@target", DbType.Single, info.Target));
            paraList.Add(SQLiteHelper.CreateParameter("@weight", DbType.Int32, info.Weight));
            paraList.Add(SQLiteHelper.CreateParameter("@contrast", DbType.Int32, info.Contrast));
            paraList.Add(SQLiteHelper.CreateParameter("@distort", DbType.Int32, info.Distort));

            SQLiteCommand command = SQLiteHelper.CreateCommand(this.STR_CONNECTION, strSql, paraList.ToArray());
            return SQLiteHelper.ExecuteNonQuery(command) > 0;
        }
        public List<ValueType> GetAllData()
        {
            string strSql = "select * from camLens";
            DataSet dsConnector = SQLiteHelper.ExecuteDataSet(STR_CONNECTION, strSql, null);
            return new Coder().DecodeListByDb(dsConnector.Tables[0], this);
        }
    }


    /// <summary>
    /// 相机镜头
    /// </summary>
    public struct CameraLens
    {
        public int Idx;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 货号
        /// </summary>
        public string Number;
        /// <summary>
        /// 相机Sensor
        /// </summary>
        public int Sensor;
        /// <summary>
        /// 视野
        /// </summary>
        public int Fov;
        /// <summary>
        /// 接口
        /// </summary>
        public int Connector;
        /// <summary>
        /// 焦距
        /// </summary>
        public float Focus;
        /// <summary>
        /// 相机法兰距（后焦距）
        /// </summary>
        public float Flange;
        /// <summary>
        /// 最小放大倍率
        /// </summary>
        public float RatioMin;
        /// <summary>
        /// 最大放大倍率
        /// </summary>
        public float RatioMax;
        /// <summary>
        /// 最大靶面
        /// </summary>
        public float Target;
        /// <summary>
        /// 重量
        /// </summary>
        public int Weight;
        /// <summary>
        /// 空间频率
        /// </summary>
        public int Contrast;
        /// <summary>
        /// 畸变量
        /// </summary>
        public int Distort;
        /// <summary>
        /// 放大倍率
        /// </summary>
        public float Ratio;
    }
}
