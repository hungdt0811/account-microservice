using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum CourseStatus
{
  NotActive = 0,
  Active = 1,
}

public enum CourseType
{
  Free = 0,
  Member = 10,
  Special = 20,
  Lecturer = 30,
  Collab = 40,
  Guild = 50,
}

public enum CourseLevel
{
  Primary = 1,
  Intermediate = 2,
  Advanced = 3,
}


