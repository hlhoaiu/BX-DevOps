using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.Views
{
    public interface ITemplatePage
    {
        void Init();
        Action<string> OnBackPage { get; set; }
        Action<string> OnNextPage { get; set; }
    }
}
