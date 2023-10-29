using Archaeoboard.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archaeoboard.UI.Models
{
    public class PostCollection
    {
        public ObservableCollection<PostDAO> Posts { get; }

        public PostCollection()
        {
            Posts = new ObservableCollection<PostDAO>();
        }
    }
}
