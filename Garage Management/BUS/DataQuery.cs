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
            using(var dbcontext = new CarModel())
            {
                if (string.IsNullOrEmpty(seacrh)) return dbcontext.Staffs.ToList();
                else return dbcontext.Staffs.Where(s => s.name.ToLower().Contains(seacrh.ToLower())).ToList();
            }
        }
 
        public bool UpdateCar(Car car, out string err)
        {
            err = string.Empty;
            try
            {
                using (var dbcontext = new CarModel())
                {
                    var updateCar = dbcontext.Cars.SingleOrDefault(s => s.idCar == car.idCar);
                    if (updateCar == null)
                    {
                        err = "Không tìm thấy sản phẩm này !";
                        return false;
                    }
                    if (updateCar.nameCar != car.nameCar) updateCar.nameCar = car.nameCar;
                    if (updateCar.idSup != car.idSup) updateCar.idSup = car.idSup;
                    if (updateCar.ngayNhap != car.ngayNhap) updateCar.ngayNhap = car.ngayNhap;
                    if (updateCar.price != car.price) updateCar.price = car.price;
                    if (updateCar.idDatHang != car.idDatHang) updateCar.idDatHang = car.idDatHang;
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
