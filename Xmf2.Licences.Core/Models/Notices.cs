﻿using System.Collections.Generic;

namespace Xmf2.Licences.Models;

public class Notices
{
    public List<Notice> AllNotices { get; } = new();

    public void AddNotice(Notice notice)
    {
        AllNotices.Add(notice);
    }
}