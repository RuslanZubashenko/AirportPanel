namespace Airport.MVC.Models.Enums
{
    using System.ComponentModel.DataAnnotations;
    public enum Sex
    {
        [Display(Name = "Male")]
        male,
        [Display(Name = "Female")]
        female
    }
}
