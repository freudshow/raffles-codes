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