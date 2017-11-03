﻿using System;
using System.Collections.Generic;
using System.Linq;
using ZdflCount.Models;
using ZdflCount.App_Start;
using Microsoft.Office.Interop.Excel;

namespace ZdflCount
{
    public class Excel
    {
        private static Application excel = new Application();

        private static void ReadStaffInfo(Range cells,int rowIdx, StaffInfo staff)
        {
            staff.Name = cells[rowIdx,1].Text.ToString();
            staff.Position = cells[rowIdx, 2].Text.ToString();
            staff.Number = cells[rowIdx, 3].Text.ToString();
            staff.DeptName = cells[rowIdx, 4].Text.ToString();
            staff.Sex = cells[rowIdx, 5].Text.ToString();
            staff.Phone = cells[rowIdx, 6].Text.ToString();
            staff.telPhone = cells[rowIdx, 7].Text.ToString();
            staff.Address = cells[rowIdx, 8].Text.ToString();
            staff.HomeAddress = cells[rowIdx, 9].Text.ToString();
            if (cells[rowIdx, 10].Text.ToString() != String.Empty)
            {
                staff.BirthDate = Convert.ToDateTime(cells[rowIdx, 10].Text);
            }
            if (cells[rowIdx, 11].Text.ToString() != String.Empty)
            {
                staff.JoinInDate = Convert.ToDateTime(cells[rowIdx, 11].Text);
            }
            staff.EmergencyName = cells[rowIdx, 12].Text.ToString();
            staff.EmergencyPhone = cells[rowIdx, 13].Text.ToString();
            staff.Remarks = cells[rowIdx, 14].Text.ToString();
        }

        public static enumErrorCode CheckAndReadStaffInfo(string filePath,List<StaffInfo> staffList)
        {
            excel.Visible = false;
            excel.UserControl = false;

            object missing = System.Reflection.Missing.Value;
            Workbook wb = excel.Application.Workbooks.Open(filePath, missing, true);
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
            int rowCount = ws.UsedRange.Cells.Rows.Count;
            int columnCount = ws.UsedRange.Cells.Columns.Count;
            if (!(ws.Cells[1, 1].Text.ToString() == "姓名" && 
                ws.Cells[1, 3].Text.ToString() == "工号" && 
                ws.Cells[1, 6].Text.ToString() == "手机"))
            {
                return enumErrorCode.ExcelHeadError;
            }
            for (int i = 2; i <= rowCount; i++)
            {
                StaffInfo staff = new StaffInfo();
                ReadStaffInfo(ws.Cells,i,staff);
                staffList.Add(staff);
            }
            return enumErrorCode.HandlerSuccess;
        }
    }
}
