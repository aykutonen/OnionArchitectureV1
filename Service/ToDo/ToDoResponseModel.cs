﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ToDoResponseModel
    {
        public class Create
        {
            public long id { get; set; }
            public string Description { get; set; }
            public int Order { get; set; }
        }

        public class Update
        {
            public long id { get; set; }
            public string Description { get; set; }
            public int Order { get; set; }
            public int Status { get; set; }
        }
    }
}
