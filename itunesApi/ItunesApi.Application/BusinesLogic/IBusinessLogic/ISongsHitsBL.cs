﻿using ItunesApi.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItunesApi.Application.BusinesLogic.IBusinessLogic
{
    public interface ISongsHitsBL
    {
        public Task<Rootobject.Songs> GetSongsHitsCall();
    }
}
