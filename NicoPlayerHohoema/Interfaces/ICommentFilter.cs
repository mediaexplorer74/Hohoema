﻿using NicoPlayerHohoema.Models.Niconico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoPlayerHohoema.Interfaces
{
    public interface ICommentFilter
    {
        bool IsFilterdComment(Comment comment);
        string TransformCommentText(string CommentText);
        bool IsIgnoreCommand(string command);
    }
}
