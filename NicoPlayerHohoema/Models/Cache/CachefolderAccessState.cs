﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoPlayerHohoema.Models.Cache
{
    public enum CacheFolderAccessState
    {
        NotAccepted,
        NotEnabled,
        NotSelected,
        SelectedButNotExist,
        Exist
    }
}
