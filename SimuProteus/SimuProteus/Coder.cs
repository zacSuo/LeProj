﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;

namespace SimuProteus
{
    /// <summary>
    /// 显示信息的编码解码
    /// </summary>
    class Coder
    {
        /// <summary>
        /// 解析单个元器件数据库信息为可用结构
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private ElementInfo DecodeComponentByDb(DataRow dr)
        {
            ElementInfo info = new ElementInfo();

            info.ID = Convert.ToInt32(dr["itemId"]);
            info.Name = dr["name"].ToString();
            info.FootType = enumComponentType.NormalComponent;
            info.Size = new Size(Convert.ToInt32(dr["width"]), Convert.ToInt32(dr["height"]));
            info.BackColor = Color.FromArgb(Convert.ToInt32(dr["backColor"]));
            info.BackImage = dr["backImage"].ToString();

            return info;
        }

        /// <summary>
        /// 解析当前画板中数据库信息为可用结构
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private ElementInfo DecodeElementByDb(DataRow dr)
        {
            ElementInfo info = new ElementInfo();

            info.Name = dr["name"].ToString();
            info.ID = Convert.ToInt32(dr["itemId"]);
            info.FootType = enumComponentType.NormalComponent;
            info.Location = new Point(Convert.ToInt32(dr["locX"]), Convert.ToInt32(dr["locY"]));
            info.Size = new Size(Convert.ToInt32(dr["width"]), Convert.ToInt32(dr["height"]));
            info.BackColor = Color.FromArgb(Convert.ToInt32(dr["backColor"]));
            info.BackImage = dr["backImage"].ToString();

            return info;
        }

        private LineFoot DecodeElementFootByDb(DataRow dr)
        {
            LineFoot info = new LineFoot();

            info.Idx = Convert.ToInt32(dr["lineIdx"]);
            info.Name = dr["name"].ToString();
            info.LocX = Convert.ToInt32(dr["locX"]);
            info.LocY = Convert.ToInt32(dr["locY"]);
            info.Color = Color.FromArgb(Convert.ToInt32(dr["color"]));

            return info;
        }

        private ElementLine DecodeCompLineByDb(DataRow dr)
        {
            ElementLine info = new ElementLine();

            info.Idx = Convert.ToInt32(dr["lineIdx"]);
            info.Name = dr["name"].ToString();
            info.oneFoot = Convert.ToInt32(dr["oneFoot"]);
            info.otherFoot = Convert.ToInt32(dr["otherFoot"]);
            info.LocX = Convert.ToInt32(dr["locX"]);
            info.LocY = Convert.ToInt32(dr["locY"]);
            info.LocOtherX = Convert.ToInt32(dr["locOtherX"]);
            info.LocOtherY = Convert.ToInt32(dr["locOtherY"]);
            info.Color = Color.FromArgb(Convert.ToInt32(dr["color"]));

            return info;
        }

        public List<LineFoot> DecodeCompFootsByDb(DataTable dt, int compIdx)
        {
            List<LineFoot> footList = new List<LineFoot>();
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["component"]) != compIdx) continue;

                footList.Add(DecodeElementFootByDb(dr));
            }
            return footList;
        }

        private ElementInfo DecodeElementInfoByDb(DataRow drComp, DataTable dtFoot, bool isComponent)
        {
            ElementInfo info ;
            if (isComponent)
            {
                info = DecodeComponentByDb(drComp);
            }
            else
            {
                info = DecodeElementByDb(drComp);
            }
            info.LineFoots = DecodeCompFootsByDb(dtFoot, info.ID);

            return info;
        }

        /// <summary>
        /// 解析读取的数据集
        /// </summary>
        /// <param name="dsComps"></param>
        /// <returns></returns>
        public List<ElementInfo> DecodeElementsByDb(DataSet dsComps, DataSet dsFoots,bool isComponent)
        {
            if (dsComps.Tables.Count < 1 || dsComps.Tables[0].Rows.Count < 1) return null;

            List<ElementInfo> infoList = new List<ElementInfo>();
            DataTable dtComps = dsComps.Tables[0];
            DataTable dtFoots = dsFoots.Tables[0];
            foreach (DataRow dr in dtComps.Rows)
            {
                infoList.Add(DecodeElementInfoByDb(dr, dtFoots, isComponent));
            }

            return infoList;
        }

        public List<ProjectInfo> DecodeProjectsByDb(DataSet ds)
        {
            List<ProjectInfo> infoList = new List<ProjectInfo>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                infoList.Add(new ProjectInfo()
                {
                    Idx = Convert.ToInt32(dr["id"]),
                    Name = dr["name"].ToString(),
                    CreateTime = Convert.ToDateTime(dr["createtime"]),
                    UpdateTime = Convert.ToDateTime(dr["updatetime"]),
                    Chips = Convert.ToInt32(dr["chips"]),
                    Description = dr["desc"].ToString()
                });

            }
            return infoList;
        }
    }
}