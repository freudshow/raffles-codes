#pragma once
#include "StdAfx.h"
#include <dbents.h>
#include "ModifyEnt.h"

class CCreateEnt
{
public:
	CCreateEnt(void);
	~CCreateEnt(void);

	static AcDbObjectId PostToModelSpace(AcDbEntity* pEnt);
	static AcDbObjectId CreateLine(AcGePoint3d ptStart, AcGePoint3d ptEnd);
	static AcDbObjectId CreateCircle(AcGePoint3d ptCenter, AcGeVector3d vec, double radius);
	static AcDbObjectId CreateCircle(AcGePoint3d ptCenter, double radius);
	static AcDbObjectId CreateCircle(AcGePoint2d pt1, AcGePoint2d pt2, AcGePoint2d pt3);
	static AcDbObjectId CreateCircleGe(AcGePoint2d pt1, AcGePoint2d pt2, AcGePoint2d pt3);
	static AcDbObjectId CreateCircle(AcGePoint2d pt1, AcGePoint2d pt2);
	static AcDbObjectId CreateArc(AcGePoint3d ptCenter, AcGeVector3d vec, double radius, double startAngle, double endAngle);
	static AcDbObjectId CreateArc(AcGePoint2d ptCenter, double radius, double startAngle, double endAngle);
	static AcDbObjectId CreateArc(AcGePoint2d ptStart, AcGePoint2d ptOnArc, AcGePoint2d ptEnd);
	static AcDbObjectId CreateArcSCE(AcGePoint2d ptStart, AcGePoint2d ptCenter, AcGePoint2d ptEnd);
	static AcDbObjectId CreateArc(AcGePoint2d ptStart, AcGePoint2d ptCenter, double angle);
	static AcDbObjectId CreatePolyline(AcGePoint2dArray points, double width);
	static AcDbObjectId CreatePolyline(AcGePoint2dArray points, double bulge, double width);
	static AcDbObjectId CreatePolyline(AcGePoint2d ptStart, AcGePoint2d ptEnd, double width);
	static AcDbObjectId Create3dPolyline(AcGePoint3dArray points);
	static AcDbObjectId CreatePolygon(AcGePoint2d ptCenter, int number, double radius, double rotation, double width);
	static AcDbObjectId CreateRectangle(AcGePoint2d pt1, AcGePoint2d pt2, double width);
	static AcDbObjectId CreatePolyCircle(AcGePoint2d ptCenter, double radius, double width);
	static AcDbObjectId CreatePolyArc(AcGePoint2d ptCenter, double radius, double angleStart, double angleEnd, double width);
	static AcDbObjectId CreateEllipse(AcGePoint3d ptCenter, AcGeVector3d vecNormal, AcGeVector3d majorAxis, double ratio);
	static AcDbObjectId CreateEllipse(AcGePoint2d pt1, AcGePoint2d pt2);
	static AcDbObjectId CreateSpline(const AcGePoint3dArray& points, int order = 4, double fitTolerance = 0.0);
	static AcDbObjectId CreateSpline(const AcGePoint3dArray& points, const AcGeVector3d& startTangent, const AcGeVector3d& endTangent, int order = 4, double fitTolerance = 0.0);
	static AcDbObjectIdArray CreateRegion(const AcDbObjectIdArray& curveIds);
	static AcDbObjectId CreateText(const AcGePoint3d& ptInsert, const ACHAR* text, AcDbObjectId style = AcDbObjectId::kNull, double height = 2.5, double rotation = 0);
	static AcDbObjectId CreateMText(const AcGePoint3d& ptInsert, const ACHAR* text, AcDbObjectId style = AcDbObjectId::kNull, double height = 2.5, double width = 0);
	static AcDbObjectId CreateHatch(AcDbObjectIdArray objIds, const ACHAR* patName, bool bAssociative);
	static AcDbObjectId CreateDimAligned(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, const ACHAR* dimText = NULL, AcDbObjectId dimStyle = AcDbObjectId::kNull);
	static AcDbObjectId CreateDimAligned(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, const AcGeVector3d& vecOffset, const ACHAR* dimText);
	static AcDbObjectId CreateDimRotated(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, double rotation, const ACHAR* dimText, AcDbObjectId dimStyle);
	static AcDbObjectId CreateDimAligned(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, const ACHAR* dimText, AcDbObjectId dimStyle);
	static AcDbObjectId CreateDimAligned(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, const AcGeVector3d& vecOffset = AcGeVector3d::kIdentity, const ACHAR* dimText = NULL);
	static AcDbObjectId CreateDimRotated(const AcGePoint3d& pt1, const AcGePoint3d& pt2, const AcGePoint3d& ptLine, double rotation, const ACHAR* dimText = NULL, AcDbObjectId dimStyle = AcDbObjectId::kNull);
	static AcDbObjectId CreateDimRadial(const AcGePoint3d& ptCenter, const AcGePoint3d& ptChord, double leaderLength, const ACHAR* dimText = NULL, AcDbObjectId dimStyle = AcDbObjectId::kNull);
};