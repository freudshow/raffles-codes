#pragma once
#include "StdAfx.h"
#include <dbents.h>

class CCalculation
{
public:
	CCalculation(void);
	~CCalculation(void);
	static AcGePoint2d MiddlePoint(AcGePoint2d pt1, AcGePoint2d pt2);
	static AcGePoint3d MiddlePoint(AcGePoint3d pt1, AcGePoint3d pt2);
	static AcGePoint3d Pt2dTo3d(AcGePoint2d pt);
	static double PI();
	static double Max(double a, double b);
	static double Min(double a, double b);
	static double RtoG(double angle);
	static double GtoR(double angle);
};