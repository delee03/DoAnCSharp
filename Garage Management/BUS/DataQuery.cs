﻿using Garage_Management.DAO;
using Garage_Management.DAO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Management.BUS
{
    public class DataQuery
    {
        CarModel context = new CarModel();
        public List<HoaDon> GetHoaDons()
        {
            return context.HoaDons.ToList();
        }

        public HoaDon FindByID(string id)
        {
            using (CarModel dbcontext = new CarModel())
            {
                return dbcontext.HoaDons.FirstOrDefault(f => f.idHoaDon == id);
             }
        }

        public void DeleteByID(string id)
        {
            HoaDon hd =  context.HoaDons.SingleOrDefault(p=> p.idHoaDon == id);
            if (hd == null)
            {
                throw new Exception("Không tìm thấy hóa đơn cần xóa!");           
            }
            else
            {
                context.HoaDons.Remove(hd);
                context.SaveChanges();
            }
        }

        public bool UpdateBill(HoaDon hd)
        {
            string err = string.Empty;
            try
            {
                using (var dbcontext = new CarModel())
                {
                    var capnhathd = context.HoaDons.SingleOrDefault(p=>p.idHoaDon == hd.idHoaDon);
                    if (capnhathd == null)
                    {
                        err = "Không tìm thấy hóa đơn này !";
                        return false;
                    }
                    if (capnhathd.tenNV != hd.tenNV) capnhathd.tenNV = hd.tenNV;
                    if (capnhathd.tenKH != hd.tenKH) capnhathd.tenKH = hd.tenKH;
                    if (capnhathd.sdt != hd.sdt) capnhathd.sdt = hd.sdt;
                    if (capnhathd.ngayLap != hd.ngayLap) capnhathd.ngayLap = hd.ngayLap;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }


        }

        public List<Staff> GetStaff()
        {
            using (CarModel context = new CarModel())
            {
                return context.Staffs.ToList();
            }
        }

        public Staff GetStaffByID(string id)
        {
            using (CarModel dbcontext = new CarModel())
            {
                return dbcontext.Staffs.FirstOrDefault(f => f.id == id);
            }
        }

        public List<Staff> GetStaff(string seacrh = "")
        {
            using (var dbcontext = new CarModel())
            {
                if (string.IsNullOrEmpty(seacrh)) return dbcontext.Staffs.ToList();
                else return dbcontext.Staffs.Where(s => s.name.ToLower().Contains(seacrh.ToLower())).ToList();
            }
        }

      

        public bool UpdateStaff(Staff staff)
        {
            string err = string.Empty;
            try
            {
                using (var dbcontext = new CarModel())
                {
                    var updateStaff = dbcontext.Staffs.SingleOrDefault(s => s.id == staff.id);
                    if (updateStaff == null)
                    {
                        err = "Không tìm thấy nhân viên này !";
                        return false;
                    }
                    if (updateStaff.Avartar_image != staff.Avartar_image) updateStaff.Avartar_image = staff.Avartar_image;
                    if (updateStaff.name != staff.name) updateStaff.name = staff.name;
                    if (updateStaff.phone != staff.phone) updateStaff.phone = staff.phone;
                    if (updateStaff.address != staff.address) updateStaff.address = staff.address;
                    dbcontext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool DeleteStaff(string staff_id)
        {
            using (var dbcontext = new CarModel())
            {
                var staff = dbcontext.Staffs.SingleOrDefault(s => s.id == staff_id);
                if (staff != null)
                {
                    dbcontext.Staffs.Remove(staff);
                    dbcontext.SaveChanges();
                    return true;
                }
                return false;
            }
        }

    }
}
