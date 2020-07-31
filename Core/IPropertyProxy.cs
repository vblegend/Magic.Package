using Magic.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic.Core
{
    public interface IPropertyProxy
    {

        void Initialize(DesignerItem designerItem);

        void Loaded(Object parent);


    }
}
