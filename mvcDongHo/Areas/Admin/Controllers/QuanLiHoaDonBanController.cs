﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvcDongHo.Areas.Admin.ViewModels;
using Application.Interfaces;
using Application.DTOs;
namespace mvcDongHo.Areas.Admin.Controllers
{
    [Area ("Admin")]
    public class QuanLiHoaDonBanController : Controller
    {
        private readonly IHoaDonBanServices _hoaDonBanServices;//khai báo services,để dùng được phải khai báo scope ở startup
        private readonly IChiTietHoaDonBanServices _chiTietHoaDonBanServices;//khai báo services,để dùng được phải khai báo scope ở startup
        public QuanLiHoaDonBanController(IHoaDonBanServices hoaDonBanServices,IChiTietHoaDonBanServices chiTietHoaDonBanServices) //contructor
        {
            _hoaDonBanServices = hoaDonBanServices;
            _chiTietHoaDonBanServices = chiTietHoaDonBanServices;
        }
        public IActionResult Index(int pageIndex = 1,string searchString="",string Type="" ,float Tien=0)//pageIndex được mặc định là 1 nếu không có truyền vào
        //pageIndex là trang hiện hành
        //searchString là chuỗi tìm kiếm
        //Type là loại mà chuỗi tìm kiếm muốn nhắm đến , ví dụ ID, Name,...
        {
            int count;//Tổng số lượng thương hiệu
            int pageSize = 3;//Số lượng thương hiệu trong 1 trang
            var list = _hoaDonBanServices.getAll(pageIndex,pageSize,searchString,Type,Tien,out count);//Hàm này lấy thương hiệu lên theo số trang , số lượng thương hiệu 1 trang , gắn lại tổng sản phẩm vào bién count
            var indexVM = new HoaDonBanView()
            {
                HoaDonBan = new PaginatedList<HoaDonBanDTO>(list,count, pageIndex, pageSize,searchString),
            };
            //Trả về view + biến indexVM đang giữ list thương hiệu
            return View(indexVM);
        }
        [HttpGet]
        public JsonResult HoaDonBan(string IDHoaDon)
        {
            var chitiet=_chiTietHoaDonBanServices.getAll();
            var list =chitiet.Where(i=>i.IDHoaDon==IDHoaDon).ToList();
            return Json(list);
            // return Json("123");
        }
    }
}
