#pragma once
#include "StdAfx.h"
#include <dbents.h>
#include "CreateEnt.h"
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
	static AcGePoint3d PolarPoint(const AcGePoint3d& pt, double angle, double distance);
	static Boolean CompareTwoPt(AcGePoint2d Pt1, AcGePoint2d Pt2);
	static AcGePoint2dArray SortByLexi(AcGePoint2dArray Points);
	static Boolean TurnRight(AcGeVector2d Vec1, AcGeVector2d Vec2);
	static Boolean TurnLeft(AcGeVector2d Vec1, AcGeVector2d Vec2);
	static AcDbObjectId ConvexHull(AcGePoint2dArray Points);
};