#include "StdAfx.h"
#include "Calculation.h"
#include <math.h>

CCalculation::CCalculation(void)
{
}

CCalculation::~CCalculation(void)
{
}

AcGePoint2d CCalculation::MiddlePoint(AcGePoint2d pt1, AcGePoint2d pt2)
{
	AcGePoint2d pt;
	pt[X] = (pt1[X] + pt2[X]) / 2;
	pt[Y] = (pt1[Y] + pt2[Y]) / 2;
	return pt;
}

AcGePoint3d CCalculation::MiddlePoint(AcGePoint3d pt1, AcGePoint3d pt2)
{
	AcGePoint3d pt;
	pt[X] = (pt1[X] + pt2[X]) / 2;
	pt[Y] = (pt1[Y] + pt2[Y]) / 2;
	pt[Z] = (pt1[Z] + pt2[Z]) / 2;
	return pt;
}

AcGePoint3d CCalculation::Pt2dTo3d(AcGePoint2d pt) 
{ 
	AcGePoint3d ptTemp(pt.x, pt.y, 0); 
	return ptTemp; 
}

double CCalculation::PI()
{
	return 4 * atan(1.0);
}

double CCalculation::Max(double a, double b) 
{ 
	if (a > b) 
	{ 
		return a; 
	} 
	else 
	{ 
		return b; 
	} 
}

double CCalculation::Min(double a, double b) 
{ 
	if (a < b) 
	{ 
		return a; 
	} 
	else 
	{ 
		return b; 
	} 
}

double CCalculation::RtoG(double angle)
{
	return angle * 180 / CCalculation::PI();
}

double CCalculation::GtoR(double angle)
{ 
	return angle * CCalculation::PI() / 180;
}

AcGePoint3d CCalculation::PolarPoint(const AcGePoint3d& pt, double angle, double distance)
{ 
	ads_point ptForm, ptTo; 
	ptForm[X] = pt.x; 
	ptForm[Y] = pt.y; 
	ptForm[Z] = pt.z; 
	acutPolar(ptForm, angle, distance, ptTo); 
	return asPnt3d(ptTo); 
}

Boolean CCalculation::CompareTwoPt(AcGePoint2d Pt1, AcGePoint2d Pt2)
{
	if(Pt1.x == Pt2.x)
		return Pt1.y < Pt2.y;
	return Pt1.x < Pt2.x;
}

AcGePoint2dArray CCalculation::SortByLexi(AcGePoint2dArray Points)
{
	// sort by x
	// lexicographically sorting
	// sort by y
	int i = 0;
	int j = 0;
	AcGePoint2d MinPt;
	AcGePoint2dArray LexiPoints;
	for (i = 0; i < Points.length(); i++)
	{
		for (j = i+1; j < Points.length(); j++)
		{
			if (CCalculation::CompareTwoPt(Points.at(i), Points.at(j)))
			{
				MinPt = Points.at(j);
			}
		}
		LexiPoints.append(MinPt);
	}
	return LexiPoints;
}

Boolean CCalculation::TurnRight(AcGeVector2d Vec1, AcGeVector2d Vec2)
{
	return ( (Vec1.x)*(Vec2.y) - (Vec1.y)*(Vec2.x) ) < 0;
}

Boolean CCalculation::TurnLeft(AcGeVector2d Vec1, AcGeVector2d Vec2)
{
	return ( (Vec1.x)*(Vec2.y) - (Vec1.y)*(Vec2.x) ) > 0;
}

AcDbObjectId CCalculation::ConvexHull(AcGePoint2dArray Points)
{
	AcDbPolyline *CHull = new AcDbPolyline();
	AcGePoint2dArray SortPoints = CCalculation::SortByLexi(Points);
	CHull->addVertexAt();
	AcDbObjectId Convexhull = CCreateEnt::PostToModelSpace(CHull);
	return Convexhull;
}