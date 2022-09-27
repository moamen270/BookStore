using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BulkyBookWeb.Controllers;
[Area("Admin")]

public class CompanyController : Controller
{

    private readonly IUnitOfWork _unitOfWork;


    public CompanyController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;       
    }

    public IActionResult Index()
    {

       
        return View();
       
    }

    //GET
    public IActionResult Upsert(int? id)
    {
       Company company = new();

        if (id == null || id == 0)
        {
            //create product
            //ViewBag.CategoryList = CategoryList;
            //ViewData["CoverTypeList"] = CoverTypeList;
            return View(company);
        }
        else
        {

            company = _unitOfWork.Company.GetFirstOrDefault(c => c.Id == id);
            return View(company);

            //update product
        }
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Company obj)
    {  
        if (ModelState.IsValid)
        {
            if(obj.Id == 0)
            {
                _unitOfWork.Company.Add(obj);
                TempData["success"] = "Company created successfully";
            }
            else
            {
                _unitOfWork.Company.Update(obj);
                TempData["success"] = "Company Updated successfully";
            }
                
            
            
            _unitOfWork.Save();

            
             return RedirectToAction("Index");
        }
        return View(obj);
    }

    //POST
    [HttpDelete]
    public IActionResult Delete(int? id)
    {
        var company = _unitOfWork.Company.GetFirstOrDefault(item => item.Id == id);
        if (company == null)
            return Json(new { success = false, message = "Error While Deleting" });

        _unitOfWork.Company.Remove(company);
        _unitOfWork.Save();
       
        return Json(new { success = true, message = "Product Deleted Successfully" });

    }






    [HttpGet]
    public IActionResult  GetAll()
    {
        var CompanyList = _unitOfWork.Company.GetAll();

        return Json(new { data = CompanyList });

    }
}