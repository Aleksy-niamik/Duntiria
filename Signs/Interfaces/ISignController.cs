﻿using Signs.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signs.Interfaces
{
    public interface ISignController
    {
        Image SignToImage(Sign sign, int radius);

        Image SignToSquare(Sign sign, int side);
    }
}
