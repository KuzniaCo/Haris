﻿using System.Collections.Generic;
using Haris.DataModel.DataModels;

namespace Haris.DataModel.Repositories
{
    public interface ICubeRepository
    {
        void CreateCube(Cube cube);
        Cube GetCube(string address);
        void UpdateCube(Cube cube);
        List<Log> GetValues(string address);
    }
}