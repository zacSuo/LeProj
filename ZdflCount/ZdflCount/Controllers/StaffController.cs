﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Mvc;
using ZdflCount.Models;
using ZdflCount.App_Start;
using System.Data.Entity;
using System.Web.Security;
using WebMatrix.WebData;


namespace ZdflCount.Controllers
{
    public class StaffController : Controller
    {
        private DbTableDbContext db = new DbTableDbContext();
        private UsersContext dbUser = new UsersContext();

        #region 列表
        [UserRoleAuthentication(Roles = "员工信息管理员,员工权限管理员,批量导入员工,单个新增员工,修改员工信息,删除员工信息")]
        public ActionResult Index(enumErrorCode error = enumErrorCode.NONE)
        {
            ViewData["error"] = Constants.GetErrorString(error);

            var itemList = from item in db.StaffInfo
                           where item.Status != enumStaffStatus.Deleted
                           orderby item.Status
                           select item;
            return View(itemList);
        }
        #endregion

        #region 详情
        [UserRoleAuthentication(Roles = "员工信息管理员,员工权限管理员,批量导入员工,单个新增员工,修改员工信息,删除员工信息")]
        public ActionResult Detail(int id)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        #endregion

        #region 新建
        [UserRoleAuthentication(Roles = "员工信息管理员,单个新增员工")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "员工信息管理员,单个新增员工")]
        public ActionResult Create(StaffInfo staffInfo)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<StaffInfo> tempStaff = from item in db.StaffInfo
                                                   where item.Number == staffInfo.Number || item.Phone == staffInfo.Phone
                                                   select item;
                if (tempStaff.Count() > 0)
                {
                    ViewData["error"] = "【工号】 或【手机号码】重复";
                    return View(staffInfo);
                }
                //存入数据库
                db.StaffInfo.Add(staffInfo);
                int a = db.SaveChanges();
            }
            return View("Detail", staffInfo);
        }

        #endregion

        #region 修改
        [UserRoleAuthentication(Roles = "员工信息管理员,修改员工信息")]
        public ActionResult Edit(int id = 0)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "员工信息管理员,修改员工信息")]
        public ActionResult Edit(StaffInfo staff)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(staff).State = EntityState.Modified;
                db.StaffInfo.Attach(staff);
                var tempEntity = db.Entry(staff);
                tempEntity.Property(item => item.Phone).IsModified = true;
                tempEntity.Property(item => item.telPhone).IsModified = true;
                tempEntity.Property(item => item.Address).IsModified = true;
                tempEntity.Property(item => item.HomeAddress).IsModified = true;
                tempEntity.Property(item => item.BirthDate).IsModified = true;
                tempEntity.Property(item => item.JoinInDate).IsModified = true;
                tempEntity.Property(item => item.EmergencyName).IsModified = true;
                tempEntity.Property(item => item.EmergencyPhone).IsModified = true;
                tempEntity.Property(item => item.Remarks).IsModified = true;

                db.SaveChanges();
                return RedirectToAction("Detail", staff);
            }
            return Content("输入修改施工单数据无效"); ;
        }

        #endregion

        #region 删除
        [UserRoleAuthentication(Roles = "员工信息管理员,删除员工信息")]
        public ActionResult Delete(int id = 0)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            if (staffInfo == null)
            {
                return HttpNotFound();
            }
            return View(staffInfo);
        }

        [HttpPost, ActionName("Delete")]
        [UserRoleAuthentication(Roles = "员工信息管理员,删除员工信息")]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffInfo staffInfo = db.StaffInfo.Find(id);
            //db.Schedules.Remove(schedules);

            db.StaffInfo.Attach(staffInfo);
            var tempEntity = db.Entry(staffInfo);
            staffInfo.Status = enumStaffStatus.Deleted;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Excel文件
        [UserRoleAuthentication(Roles = "员工信息管理员,批量导入员工")]
        public ActionResult DownloadTemplate()
        {
            FilePathResult file = new FilePathResult("~/Downloads/员工信息.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            file.FileDownloadName = "员工信息.xlsx";
            return file;
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "员工信息管理员,批量导入员工")]
        public ActionResult UploadStaffInfo(HttpPostedFileBase excelFileName)
        {
            if (excelFileName.ContentLength <= 0)
            {
                return RedirectToAction("Index");
            }
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetFileName(excelFileName.FileName);
            string extName = Path.GetExtension(excelFileName.FileName);
            if (extName != ".xlsx")
            {
                return RedirectToAction("Index", new { error = enumErrorCode.FileOnlyExcel });
            }
            string serverPath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            excelFileName.SaveAs(serverPath);
            List<StaffInfo> staffList = new List<StaffInfo>();
            enumErrorCode result = Excel.CheckAndReadStaffInfo(serverPath, staffList);

            if (result == enumErrorCode.NONE)
            {
                foreach (StaffInfo staff in staffList)
                {
                    db.StaffInfo.Add(staff);
                }
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    result = enumErrorCode.ExcelContentError;
                }
            }
            object tempObj = result == enumErrorCode.NONE ? null : new { error = result };
            return RedirectToAction("Index", tempObj);
        }
        #endregion

        #region 角色管理

        [ChildActionOnly]
        [UserRoleAuthentication(Roles = "员工权限管理员")]
        public ActionResult GetStaff()
        {
            return PartialView("_DetailPartial");
        }

        [UserRoleAuthentication(Roles = "员工权限管理员")]
        public ActionResult RoleIndex()
        {
            string[] roleArray = Enum.GetNames(typeof(enumUserRole));
            return View(roleArray);
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "员工权限管理员")]
        public string GetStaffInfo(string number)
        {
            StringBuilder strBuiler = new StringBuilder ();
            //读取员工信息
            IEnumerable<StaffInfo> staffList = from item in db.StaffInfo
                              where item.Number == number && item.Status == enumStaffStatus.Normal
                              select item;
            if(staffList.Count() < 1){
                return string.Empty;
            }
            StaffInfo staff = staffList.First();
            strBuiler.Append(staff.Name); strBuiler.Append(";");
            strBuiler.Append(staff.Number); strBuiler.Append(";");
            strBuiler.Append(staff.DeptName); strBuiler.Append(";");
            strBuiler.Append(staff.Position); strBuiler.Append(";");
            //同步管理账户
            IEnumerable<UserProfile> userList = from item in dbUser.UserProfiles
                                                where item.UserName == staff.Number
                                                select item;
            if (userList.Count() < 1)
            {
                WebSecurity.CreateUserAndAccount(staff.Number, staff.Phone);
            }
            //读取管理权限
            string[] roleArray = Enum.GetNames(typeof(enumUserRole));
            strBuiler.Append(roleArray.Length); strBuiler.Append(";");
            string tempRole = "0";
            for (int i = 1; i < roleArray.Length; i++)
            {
                tempRole = Roles.IsUserInRole(staff.Number,roleArray[i])?"1":"0";
                strBuiler.Append(tempRole);
            }

            return strBuiler.ToString();
        }

        [HttpPost]
        [UserRoleAuthentication(Roles = "员工权限管理员")]
        public ActionResult SetStaffRole()
        {
            string[] roleArray = Enum.GetNames(typeof(enumUserRole));
            string tempStatus, userName = Request.Form["number"];
            List<string> addRoles = new List<string>(), removeRoles = new List<string>();
            foreach (string item in roleArray)
            {
                tempStatus = Request.Form[item];
                if (tempStatus == null)
                {
                    continue;
                }
                else if (tempStatus.IndexOf(",false") > 0 && !Roles.IsUserInRole(userName, item))
                {
                    addRoles.Add(item);
                }
                else if (tempStatus == "false" && Roles.IsUserInRole(userName, item))
                {
                    removeRoles.Add(item);
                }
            }
            if (addRoles.Count > 0)
            {
                Roles.AddUserToRoles(userName, addRoles.ToArray());
            }
            if (removeRoles.Count > 0)
            {
                Roles.RemoveUserFromRoles(userName, removeRoles.ToArray());
            }
            ViewData["alert"] = "授权成功";
            return View("RoleIndex", roleArray);
        }

        #endregion
    }
}
