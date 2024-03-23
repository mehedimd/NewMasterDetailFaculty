using NewMasterDetailFaculty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewMasterDetailFaculty.Controllers
{
    public class FacultyController : Controller
    {
        NewFacultyStudentDBEntities db = new NewFacultyStudentDBEntities();
        // GET: Faculty
        public ActionResult Index()
        {            
            return View(db.Faculties.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Faculty faculty)
        {
            if (ModelState.IsValid && faculty.Students != null && faculty.Students.Count > 0)
            {
                if (faculty.Picture != null)
                {
                    var ext = Path.GetExtension(faculty.Picture.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".svg")
                    {
                        var rootPath = Server.MapPath("~/");
                        var fileToSave = Path.Combine(rootPath, "Pictures", faculty.Picture.FileName);
                        faculty.Picture.SaveAs(fileToSave);
                        faculty.PicPath = "~/Pictures/" + faculty.Picture.FileName;
                        db.Faculties.Add(faculty);
                        if (db.SaveChanges() > 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Provide a Valid Error");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("","Please Add atleast 1 Student");
                return View(faculty);
            }
            return View(faculty);
        }
        public ActionResult Edit(int id)
        {
            return View(db.Faculties.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Faculty faculty)
        {
            var oldFaculty = db.Faculties.Find(faculty.ID);
            if (ModelState.IsValid && faculty.Students != null && faculty.Students.Count > 0)
            {
                if (faculty.Picture != null)
                {
                    var ext = Path.GetExtension(faculty.Picture.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".svg")
                    {
                        var rootPath = Server.MapPath("~/");
                        var fileToSave = Path.Combine(rootPath, "Pictures", faculty.Picture.FileName);
                        faculty.Picture.SaveAs(fileToSave);
                        faculty.PicPath = "~/Pictures/" + faculty.Picture.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Please Provide a Valid Error");
                    }
                }
                else
                {
                    oldFaculty.PicPath = oldFaculty.PicPath;
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Add atleast 1 Student");
                return View(faculty);
            }
            oldFaculty.FacultyName = faculty.FacultyName;
            oldFaculty.CourseName = faculty.CourseName;
            oldFaculty.PhoneNumber = faculty.PhoneNumber;
            oldFaculty.CourseStartDate = faculty.CourseStartDate;
            oldFaculty.IsRunning = faculty.IsRunning;
            db.Students.RemoveRange(db.Students.Where(s => s.FacultyID.Equals(oldFaculty.ID)));
            db.SaveChanges();
            oldFaculty.Students = faculty.Students;
            db.Entry(oldFaculty).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            return View(faculty);
        }
        // Delete Action
        public ActionResult Delete(int id)
        {
            db.Faculties.Remove(db.Faculties.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}