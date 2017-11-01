﻿using System;
using System.Text;
using ZdflCount.Models;

namespace ZdflCount.App_Start
{
    /// <summary>
    /// 处理来自设备端的数据
    /// </summary>
    public interface interfaceClientHanlder
    {
        byte[] HandlerClientData(byte[] buff);
        bool ShouldResponse();
    }

    /// <summary>
    /// 生产数据
    /// </summary>
    public class ClientHanlderProductInfo : interfaceClientHanlder
    {
        private int decodeByte2Int(byte[] buff,ref int startIdx,int length)
        {
            int result = 0;
            byte[] tempByte = new byte[length];
            Array.Copy(buff, startIdx, tempByte, 0, length);
            if (length == 2)
            {
                result = ConvertHelper.BytesToInt16(tempByte, true);
            }
            else if (length == 4)
            {
                result = ConvertHelper.BytesToInt32(tempByte, true);
            }
            startIdx += length;
            return result;
        }
        /// <summary>
        /// 上传生产情况解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private ProductInfo DecodeData(byte[] buff)
        {
            ProductInfo info = new ProductInfo();
            byte[] tempData = buff;
            //机器码
            int locIdx = 0;
            info.MachineId = decodeByte2Int(buff,ref locIdx, 2);
            //施工单编码
            int tempLen = tempData[locIdx++];
            byte[] scheduleByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, scheduleByte, 0, tempLen);
            info.ScheduleNumber = Encoding.ASCII.GetString(scheduleByte);
            locIdx += tempLen;
            //通道数
            info.ChannelCount = tempData[locIdx++];
            //工号
            tempLen = tempData[locIdx++];
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.StaffNumber = Encoding.ASCII.GetString(numberByte);
            locIdx += tempLen;
            //姓名
            tempLen = tempData[locIdx++];
            byte[] nameByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, nameByte, 0, tempLen);
            info.StaffName = Encoding.GetEncoding("GBK").GetString(nameByte);
            locIdx += tempLen;
            //状态标志位
            info.MsgStatus = tempData[locIdx++];
            //各通道已完成数
            info.ChannelFinish1 = decodeByte2Int(buff,ref locIdx, 4);
            info.ChannelFinish2 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish3 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish4 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish5 = decodeByte2Int(buff, ref locIdx, 4);
            info.ChannelFinish6 = decodeByte2Int(buff, ref locIdx, 4);
            //异常数
            info.UnusualCount = decodeByte2Int(buff, ref locIdx, 4);

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private Models.ProductInfo exchangeData(ProductInfo info, Machines machine)
        {

            return new Models.ProductInfo()
            {
                DateCreate = DateTime.Now,
                ChannelCount = info.ChannelCount,

                staffId = info.StaffNumber,
                StaffName = info.StaffName,

                MachineIP = machine.IpAddress,
                MachineId = machine.ID,
                MachineName = machine.Name,

                StaffStatus = info.MsgStatus,

                ChannelFinish1 = info.ChannelFinish1,
                ChannelFinish2 = info.ChannelFinish2,
                ChannelFinish3 = info.ChannelFinish3,
                ChannelFinish4 = info.ChannelFinish4,
                ChannelFinish5 = info.ChannelFinish5,
                ChannelFinish6 = info.ChannelFinish6,

                ExceptionCount = info.UnusualCount
            };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            ProductInfo outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.ProductInfo innerInfo = this.exchangeData(outInfo, machine);

            //记录原始数据
            db.ProductInfo.Add(innerInfo);
            db.SaveChanges();


            byte[] buffResp = { 0 };
            return buffResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 心跳数据
    /// </summary>
    public class ClientHandlerHeartBreak : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public HeartBreak DecodeData(byte[] buff)
        {
            HeartBreak info = new HeartBreak();
            byte[] tempData = buff;
            byte[] machineByte = new byte[2];
            Array.Copy(tempData, machineByte, 2);
            info.MachineId = ConvertHelper.BytesToInt16(machineByte, true);
            info.ChannelInfo = buff[2];

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <param name="machine"></param>
        /// <returns></returns>
        private Models.HeartBreak exchangeData(HeartBreak info, Machines machine)
        {
            return new Models.HeartBreak()
            {
                DateCreate = DateTime.Now,
                ChannelInfo = info.ChannelInfo,
                MachineId = machine.ID,
                MachineName = machine.Number
            };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            HeartBreak outInfo = this.DecodeData(buff);
            Machines machine = db.Machines.Find(outInfo.MachineId);
            Models.HeartBreak innerInfo = this.exchangeData(outInfo, machine);

            //记录原始数据
            db.HeartBreak.Add(innerInfo);
            db.SaveChanges();
            
            return null;
        }

        public bool ShouldResponse()
        {
            return false;
        }
    }

    /// <summary>
    /// 设备设置
    /// </summary>
    public class ClientHandlerDeviceSetting : interfaceClientHanlder
    {
        /// <summary>
        /// 客户端返回消息解码
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        public DeviceSetting DecodeData(byte[] buff)
        {
            DeviceSetting info = new DeviceSetting();
            byte[] tempData = buff;

            info.OperateType = buff[0];
            int locIdx = 2, tempLen = buff[1];
            //设备编码
            byte[] numberByte = new byte[tempLen];
            Array.Copy(tempData,locIdx, numberByte,0, tempLen);
            info.DeviceNumber = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //设备备注名
            tempLen = buff[locIdx++];
            numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.DeviceName = System.Text.Encoding.GetEncoding("GBK").GetString(numberByte);
            locIdx += tempLen;
            //IP地址
            tempLen = 15;
            numberByte = new byte[tempLen];
            Array.Copy(tempData, locIdx, numberByte, 0, tempLen);
            info.IPAddress = System.Text.Encoding.ASCII.GetString(numberByte);

            return info;
        }

        /// <summary>
        /// 内外部数据转换
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Machines exchangeData(DeviceSetting info)
        {
            return new Machines()
            {
                IpAddress = info.IPAddress,
                Number = info.DeviceNumber,
                Name = info.DeviceName,
                Status = enumMachineStatus.Normal
            };
        }

        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();
            DeviceSetting outInfo = this.DecodeData(buff);
            Machines innerInfo = this.exchangeData(outInfo);

            //记录原始数据
            db.Machines.Add(innerInfo);
            db.SaveChanges();
            //生成返回结果
            byte[] byteID = ConvertHelper.Int16ToBytes(innerInfo.ID, true);
            byte[] byteResp = new byte[3];
            byteResp[0] = 1;
            Array.Copy(byteID, 0, byteResp, 1, 2);
            return byteResp;
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }

    /// <summary>
    /// 命令错误，默认处理方式
    /// </summary>
    public class ClientHandlerNoneDefault : interfaceClientHanlder
    {
        public byte[] HandlerClientData(byte[] buff)
        {
            DbTableDbContext db = new DbTableDbContext();

            db.RecordErrorInfo(enumSystemErrorCode.TcpDefaultHandlerErr, "", buff);

            return new byte[] { 1 };
        }

        public bool ShouldResponse()
        {
            return true;
        }
    }
}


