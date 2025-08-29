using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class PostOffice
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string BranchType { get; set; }
    public string DeliveryStatus { get; set; }
    public string Taluk { get; set; }
    public string Circle { get; set; }
    public string District { get; set; }
    public string Division { get; set; }
    public string Region { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}

public class PostOfficeList
{
    public string Message { get; set; }
    public string Status { get; set; }
    public List<PostOffice> PostOffice { get; set; }
}