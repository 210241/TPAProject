﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Serialization;
using DataLayer.DataModel;

namespace FileData.XMLModel
{
    
    [Export(typeof(BaseAssemblyModel))]
    public class XMLAssemblyModel : BaseAssemblyModel
    {
        public override string Name { get; set; }


         public new List<XMLNamespaceModel> NamespaceModels { get; set; }

    }
}
