﻿using System;

namespace ZdflCount.App_Start
{
    /// <summary>
    /// 数据格式框架
    /// </summary>
    public struct NormalDataStruct
    {
        public int FactoryNumber;
        public enumCommandType Code;
        public int contentLen;
        public int FillField;
        public byte[] Content;
    }

    /// <summary>
    /// 生产情况
    /// </summary>
    public struct ProductInfo
    {
        public int MachineId;
        public string ScheduleNumber;
        public int ChannelCount;
        public string StaffNumber;
        public string StaffName;
        public byte MsgStatus;
        public int ChannelFinish1;
        public int ChannelFinish2;
        public int ChannelFinish3;
        public int ChannelFinish4;
        public int ChannelFinish5;
        public int ChannelFinish6;
        public int UnusualCount;
    }

    /// <summary>
    /// 心跳
    /// </summary>
    public struct HeartBreak
    {
        public int MachineId;
        public int ChannelInfo;
    }

    /// <summary>
    /// 设备设置
    /// </summary>
    public struct DeviceSetting
    {
        public int OperateType;
        public string DeviceNumber;
        public string DeviceName;
        public string IPAddress;
    }
}
