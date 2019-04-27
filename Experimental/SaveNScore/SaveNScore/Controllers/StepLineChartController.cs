using SaveNScore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaveNScore.Controllers
{
    public class StepLineChartController : Controller
    {
        // GET: StepLineChart
        public ActionResult Index()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();


            dataPoints.Add(new DataPoint(1388514600000, 102.1));
            dataPoints.Add(new DataPoint(1391193000000, 104.83));
            dataPoints.Add(new DataPoint(1393612200000, 104.04));
            dataPoints.Add(new DataPoint(1396290600000, 104.87));
            dataPoints.Add(new DataPoint(1398882600000, 105.71));
            dataPoints.Add(new DataPoint(1401561000000, 108.37));
            dataPoints.Add(new DataPoint(1404153000000, 105.23));
            dataPoints.Add(new DataPoint(1406831400000, 100.05));
            dataPoints.Add(new DataPoint(1409509800000, 95.85));
            dataPoints.Add(new DataPoint(1412101800000, 86.08));
            dataPoints.Add(new DataPoint(1414780200000, 76.99));
            dataPoints.Add(new DataPoint(1417372200000, 60.7));
            dataPoints.Add(new DataPoint(1420050600000, 47.11));
            dataPoints.Add(new DataPoint(1422729000000, 54.79));
            dataPoints.Add(new DataPoint(1425148200000, 52.83));
            dataPoints.Add(new DataPoint(1427826600000, 57.54));
            dataPoints.Add(new DataPoint(1430418600000, 62.51));
            dataPoints.Add(new DataPoint(1433097000000, 61.31));
            dataPoints.Add(new DataPoint(1435689000000, 54.34));
            dataPoints.Add(new DataPoint(1438367400000, 45.69));
            dataPoints.Add(new DataPoint(1441045800000, 46.28));
            dataPoints.Add(new DataPoint(1443637800000, 46.96));
            dataPoints.Add(new DataPoint(1446316200000, 43.11));
            dataPoints.Add(new DataPoint(1448908200000, 36.57));
            dataPoints.Add(new DataPoint(1451586600000, 29.78));
            dataPoints.Add(new DataPoint(1454265000000, 31.03));
            dataPoints.Add(new DataPoint(1456770600000, 37.34));
            dataPoints.Add(new DataPoint(1459449000000, 40.75));
            dataPoints.Add(new DataPoint(1462041000000, 45.94));
            

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return PartialView();
        }
    }
}