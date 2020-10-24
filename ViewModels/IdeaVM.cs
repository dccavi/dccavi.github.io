using System;
using System.ComponentModel.DataAnnotations;

namespace SongStorm.ViewModels
{
    public class IdeaVM
    {
        [Display(Name = "Your Idea: ")]
        public string IdeaIn { get; set; }

        [Display(Name = "Freewrite Space ")]
        public string Freewrite { get; set; }

        [Display(Name = "Freewrite Space ")]
        public string Notes { get; set; }

        public SidebarVM Sidebar { get; set; }
    }
    
}
