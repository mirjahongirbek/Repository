﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryRule.Entity
{
    public interface IUserDevice<TKey>:IEntity<TKey>
    {
        string DeviceId { get; set; }
        string DeviceName { get; set; }
        TKey UserId { get; set; }
        string AccessToken { get; set; }
        string RefreshToken { get; set; }
        DateTime AccessTime { get; set; }
    }
}
