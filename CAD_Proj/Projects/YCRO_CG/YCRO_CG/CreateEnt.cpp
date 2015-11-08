#include "StdAfx.h"
#include "CreateEnt.h"
#include "Calculation.h"

CCreateEnt::CCreateEnt(void)
{
}

CCreateEnt::~CCreateEnt(void)
{
}

AcDbObjectId CCreateEnt::CreateLine(AcGePoint3d ptStart, AcGePoint3d ptEnd)
{
	AcDbLine *pLine = new AcDbLine(ptStart, ptEnd);
	AcDbObjectId lineId;
	lineId = PostToModelSpace(pLine);
	return lineId;
}

AcDbObjectId CCreateEnt::PostToModelSpace(AcDbEntity* pEnt)
{
	AcDbDatabase *pDB = acdbHostApplicationServices()->workingDatabase();
	AcDbBlockTable *pBlockTable;
	AcDbBlockTableRecord *pBlockTableRecord;
	pDB->getBlockTable(pBlockTable, AcDb::kForRead);
	pBlockTable->getAt(ACDB_MODEL_SPACE, pBlockTableRecord, AcDb::kForWrite);
	AcDbObjectId entId;
	pBlockTableRecord->appendAcDbEntity(entId, pEnt);
	pBlockTable->close();
	pBlockTableRecord->close();
	pEnt->close();
	return entId;
}

AcDbObjectId CCreateEnt::CreateCircle(AcGePoint3d ptCenter, AcGeVector3d vec, double radius)
{
	AcDbCircle *pCircle = new AcDbCircle(ptCenter, vec, radius);
	AcDbObjectId circleId;
	circleId = CCreateEnt::PostToModelSpace(pCircle);
	return circleId;
}

AcDbObjectId CCreateEnt::CreateCircle(AcGePoint3d ptCenter, double radius)
{
	AcGeVector3d vec(0, 0, 1);
	return CCreateEnt::CreateCircle(ptCenter, vec, radius);
}

AcDbObjectId CCreateEnt::CreateCircle(AcGePoint2d pt1, AcGePoint2d pt2, AcGePoint2d pt3)
{
	double xysm, xyse, xy;
	AcGePoint3d ptCenter;
	double radius;
	xy = pow(pt1[X], 2) + pow(pt1[Y], 2);
	xyse = xy - pow(pt3[X], 2) - pow(pt3[Y], 2);
	xysm = xy - pow(pt2[X], 2) - pow(pt2[Y], 2);
	xy = (pt1[X] - pt2[X]) * (pt1[Y] - pt3[Y]) - (pt1[X] - pt3[X]) * (pt1[Y] - pt2[Y]); 

	if (fabs(xy) < 0.000001)
	{
		acutPrintf(_T("所输入的参数无法创建圆形!"));
		return 0;
	}

	ptCenter[X] = (xysm * (pt1[Y] - pt3[Y]) - xyse * (pt1[Y] - pt2[Y])) / (2 * xy);
	ptCenter[Y] = (xyse * (pt1[X] - pt2[X]) - xysm * (pt1[X] - pt3[X])) / (2 * xy);
	ptCenter[Z] = 0;

	radius = sqrt((pt1[X] - ptCenter[X]) * (pt1[X] - ptCenter[X]) + (pt1[Y] - ptCenter[Y]) * (pt1[Y] - ptCenter[Y]));
	if (radius < 0.000001)
	{
		acutPrintf(_T("半径过小!"));
		return 0;
	}

	return CCreateEnt::CreateCircle(ptCenter, radius);
}

AcDbObjectId CCreateEnt::CreateCircleGe(AcGePoint2d pt1, AcGePoint2d pt2, AcGePoint2d pt3)
{
	AcGeCircArc2d geArc(pt1, pt2, pt3);
	AcGePoint3d ptCenter(geArc.center().x, geArc.center().y, 0);
	return CCreateEnt::CreateCircle(ptCenter, geArc.radius());
}

AcDbObjectId CCreateEnt::CreateCircle(AcGePoint2d pt1, AcGePoint2d pt2) 
{
	AcGePoint2d pt = CCalculation::MiddlePoint(pt1, pt2); 
	AcGePoint3d ptCenter(pt[X], pt[Y], 0);
	double radius = pt1.distanceTo(pt2) / 2;
	return CCreateEnt::CreateCircle(ptCenter, radius);
}

AcDbObjectId CCreateEnt::CreateArc(AcGePoint3d ptCenter, AcGeVector3d vec, double radius, double startAngle, double endAngle)
{ 
	AcDbArc *pArc = new AcDbArc(ptCenter, vec, radius, startAngle, endAngle); 
	AcDbObjectId arcId; 
	arcId = CCreateEnt::PostToModelSpace(pArc);
	return arcId;
}

AcDbObjectId CCreateEnt::CreateArc(AcGePoint2d ptCenter, double radius, double startAngle, double endAngle)
{
	AcGeVector3d vec(0, 0, 1);
	return CCreateEnt::CreateArc(CCalculation::Pt2dTo3d(ptCenter), vec, radius, startAngle, endAngle);
}

AcDbObjectId CCreateEnt::CreateArc(AcGePoint2d ptStart, AcGePoint2d ptOnArc, AcGePoint2d ptEnd)
{
	AcGeCircArc2d geArc(ptStart, ptOnArc, ptEnd);
	AcGePoint2d ptCenter = geArc.center();
	double radius = geArc.radius();
	AcGeVector2d vecStart(ptStart.x - ptCenter.x, ptStart.y - ptCenter.y);
	AcGeVector2d vecEnd(ptEnd.x - ptCenter.x, ptEnd.y - ptCenter.y);
	double startAngle = vecStart.angle();
	double endAngle = vecEnd.angle();
	return CCreateEnt::CreateArc(ptCenter, radius, startAngle, endAngle);
}

AcDbObjectId CCreateEnt::CreateArcSCE(AcGePoint2d ptStart, AcGePoint2d ptCenter, AcGePoint2d ptEnd)
{
	double radius = ptCenter.distanceTo(ptStart);
	AcGeVector2d vecStart(ptStart.x - ptCenter.x, ptStart.y - ptCenter.y);
	AcGeVector2d vecEnd(ptEnd.x - ptCenter.x, ptEnd.y - ptCenter.y);
	double startAngle = vecStart.angle();
	double endAngle = vecEnd.angle();
	return CCreateEnt::CreateArc(ptCenter, radius, startAngle, endAngle);
}

AcDbObjectId CCreateEnt::CreateArc(AcGePoint2d ptStart, AcGePoint2d ptCenter, double angle)
{
	double radius = ptCenter.distanceTo(ptStart);
	AcGeVector2d vecStart(ptStart.x - ptCenter.x, ptStart.y - ptCenter.y);
	double startAngle = vecStart.angle();
	double endAngle = startAngle + angle;
	return CCreateEnt::CreateArc(ptCenter, radius, startAngle, endAngle);
}

AcDbObjectId CCreateEnt::CreatePolyline(AcGePoint2dArray points, double width)
{
	int numVertices = points.length();
	AcDbPolyline *pPoly = new AcDbPolyline(numVertices);
	for (int i = 0; i < numVertices; i++) 
	{
			pPoly->addVertexAt(i, points.at(i), 0, width, width);
	}
	AcDbObjectId polyId;
	polyId = CCreateEnt::PostToModelSpace(pPoly);
	return polyId;
}

AcDbObjectId CCreateEnt::CreatePolyline(AcGePoint2dArray points, double bulge, double width)
{
	int numVertices = points.length();
	AcDbPolyline *pPoly = new AcDbPolyline(numVertices);
	for (int i = 0; i < numVertices; i++) 
	{
		pPoly->addVertexAt(i, points.at(i), bulge, width, width);
	}
	AcDbObjectId polyId;
	polyId = CCreateEnt::PostToModelSpace(pPoly);
	return polyId;
}

AcDbObjectId CCreateEnt::CreatePolyline(AcGePoint2d ptStart, AcGePoint2d ptEnd, double width) 
{ 
	AcGePoint2dArray points;
	points.append(ptStart);
	points.append(ptEnd);
	return CCreateEnt::CreatePolyline(points, width);
}

AcDbObjectId CCreateEnt::Create3dPolyline(AcGePoint3dArray points) 
{
	AcDb3dPolyline *pPoly3d = new AcDb3dPolyline(AcDb::k3dSimplePoly, points);
	return CCreateEnt::PostToModelSpace(pPoly3d);
}

AcDbObjectId CCreateEnt::CreatePolygon(AcGePoint2d ptCenter, int number, double radius, double rotation, double width)
{ 
	AcGePoint2dArray points;
	double angle = 2 * CCalculation::PI() / (double)number;
	for (int i = 0; i < number; i++)
	{
		AcGePoint2d pt;
		pt.x = ptCenter.x + radius * cos(i * angle);
		pt.y = ptCenter.y + radius * sin(i * angle);
		points.append(pt);
	}
	AcDbObjectId polyId = CCreateEnt::CreatePolyline(points, width);
	AcDbEntity *pEnt;
	acdbOpenAcDbEntity(pEnt, polyId, AcDb::kForWrite);
	AcDbPolyline *pPoly = AcDbPolyline::cast(pEnt);
	if (pPoly != NULL)
	{
		pPoly->setClosed(Adesk::kTrue);
	}
	pEnt->close();
	CModifyEnt::Rotate(polyId, ptCenter, rotation);
	return polyId;
}

AcDbObjectId CCreateEnt::CreateRectangle(AcGePoint2d pt1, AcGePoint2d pt2, double width) 
{
	double x1 = pt1.x, x2 = pt2.x; 
	double y1 = pt1.y, y2 = pt2.y; 

	AcGePoint2d ptLeftBottom(CCalculation::Min(x1, x2), CCalculation::Min(y1, y2)); 
	AcGePoint2d ptRightBottom(CCalculation::Max(x1, x2), CCalculation::Min(y1, y2)); 
	AcGePoint2d ptRightTop(CCalculation::Max(x1, x2), CCalculation::Max(y1, y2)); 
	AcGePoint2d ptLeftTop(CCalculation::Min(x1, x2), CCalculation::Max(y1, y2)); 

	AcDbPolyline *pPoly = new AcDbPolyline(4); 
	pPoly->addVertexAt(0, ptLeftBottom, 0, width, width); 
	pPoly->addVertexAt(1, ptRightBottom, 0, width, width); 
	pPoly->addVertexAt(2, ptRightTop, 0, width, width); 
	pPoly->addVertexAt(3, ptLeftTop, 0, width, width); 
	pPoly->setClosed(Adesk::kTrue); 

	AcDbObjectId polyId; 
	polyId = CCreateEnt::PostToModelSpace(pPoly); 
	return polyId; 
}

AcDbObjectId CCreateEnt::CreatePolyCircle(AcGePoint2d ptCenter, double radius, double width) 
{
	AcGePoint2d pt1, pt2, pt3; 
	pt1.x = ptCenter.x + radius; 
	pt1.y = ptCenter.y; 
	pt2.x = ptCenter.x - radius; 
	pt2.y = ptCenter.y; 
	pt3.x = ptCenter.x + radius; 
	pt3.y = ptCenter.y;
	AcDbPolyline *pPoly = new AcDbPolyline(3);
	pPoly->addVertexAt(0, pt1, 1, width, width); 
	pPoly->addVertexAt(1, pt2, 1, width, width); 
	pPoly->addVertexAt(2, pt3, 1, width, width); 
	pPoly->setClosed(Adesk::kTrue);
	AcDbObjectId polyId; 
	polyId = CCreateEnt::PostToModelSpace(pPoly); 
	return polyId; 
}

AcDbObjectId CCreateEnt::CreatePolyArc(AcGePoint2d ptCenter, double radius, double angleStart, double angleEnd, double width) 
{
	AcGePoint2d pt1, pt2;
	pt1.x = ptCenter.x + radius * cos(angleStart); 
	pt1.y = ptCenter.y + radius * sin(angleStart);
	pt2.x = ptCenter.x + radius * cos(angleEnd);
	pt2.y = ptCenter.y + radius * sin(angleEnd);
	AcDbPolyline *pPoly = new AcDbPolyline(3);
	pPoly->addVertexAt(0, pt1, tan((angleEnd - angleStart) / 4), width, width); 
	pPoly->addVertexAt(1, pt2, 0, width, width); 
	AcDbObjectId polyId; 
	polyId = CCreateEnt::PostToModelSpace(pPoly); 
	return polyId; 
}

AcDbObjectId CCreateEnt::CreateEllipse(AcGePoint3d ptCenter, AcGeVector3d vecNormal, AcGeVector3d majorAxis, double ratio)
{ 
	AcDbEllipse *pEllipse = new AcDbEllipse(ptCenter, vecNormal, majorAxis, ratio); 
	return CCreateEnt::PostToModelSpace(pEllipse); 
}

AcDbObjectId CCreateEnt::CreateEllipse(AcGePoint2d pt1, AcGePoint2d pt2) 
{
	AcGePoint3d ptCenter; 
	ptCenter = CCalculation::MiddlePoint(CCalculation::Pt2dTo3d(pt1), CCalculation::Pt2dTo3d(pt2)); 
	AcGeVector3d vecNormal(0, 0, 1); 
	AcGeVector3d majorAxis(fabs(pt1.x - pt2.x) / 2, 0, 0); 
	double ratio = fabs((pt1.y - pt2.y) / (pt1.x - pt2.x)); 
	return CCreateEnt::CreateEllipse(ptCenter, vecNormal, majorAxis, ratio); 
}

AcDbObjectId CCreateEnt::CreateSpline(const AcGePoint3dArray& points, int order, double fitTolerance) 
{
	assert (order >= 2 && order <= 26);
	AcDbSpline *pSpline = new AcDbSpline(points, order, fitTolerance);
	AcDbObjectId splineId;
	splineId = CCreateEnt::PostToModelSpace(pSpline);
	return splineId;
}


AcDbObjectId CCreateEnt::CreateSpline(const AcGePoint3dArray& points, const AcGeVector3d& startTangent, const AcGeVector3d& endTangent, int order, double fitTolerance) 
{ 
	assert(order >= 2 && order <= 26); 
	AcDbSpline *pSpline = new AcDbSpline(points, startTangent, endTangent, order, fitTolerance); 
	return CCreateEnt::PostToModelSpace(pSpline); 
}

AcDbObjectIdArray CCreateEnt::CreateRegion(const AcDbObjectIdArray& curveIds)
{ 
	AcDbObjectIdArray regionIds;
	AcDbVoidPtrArray curves;
	AcDbVoidPtrArray regions;
	AcDbEntity *pEnt;
	AcDbRegion *pRegion;
	int i = 0;
	for (i = 0; i < curveIds.length(); i++) 
	{ 
		acdbOpenAcDbEntity(pEnt, curveIds.at(i), AcDb::kForRead); 
		if (pEnt->isKindOf(AcDbCurve::desc())) 
		{ 
			curves.append(static_cast<void*>(pEnt)); 
		} 
	}

	Acad::ErrorStatus es = AcDbRegion::createFromCurves(curves, regions); 
	if (es == Acad::eOk) 
	{
		for (i = 0; i < regions.length(); i++) 
		{
			pRegion = static_cast<AcDbRegion*>(regions[i]); 
			pRegion->setDatabaseDefaults(); 
			AcDbObjectId regionId; 
			regionId = CCreateEnt::PostToModelSpace(pRegion); 
			regionIds.append(regionId); 
		} 
	} 
	else
	{ 
		for (i = 0; i < regions.length(); i++) 
		{ 
			delete (AcRxObject*)regions[i]; 
		} 
	} 

	for (i = 0; i < curves.length(); i++) 
	{ 
		pEnt = static_cast<AcDbEntity*>(curves[i]); 
		pEnt->close(); 
	}

	return regionIds; 
}

AcDbObjectId CCreateEnt::CreateText(const AcGePoint3d& ptInsert, const ACHAR* text, AcDbObjectId style, double height, double rotation) 
{ 
	AcDbText *pText = new AcDbText(ptInsert, text, style, height, rotation);
	return CCreateEnt::PostToModelSpace(pText);
}

AcDbObjectId CCreateEnt::CreateMText(const AcGePoint3d& ptInsert, const ACHAR* text, AcDbObjectId style, double height, double width) 
{
	AcDbMText *pMText = new AcDbMText();
	pMText->setTextStyle(style);
	pMText->setContents(text);
	pMText->setLocation(ptInsert);
	pMText->setTextHeight(height);
	pMText->setWidth(width);
	pMText->setAttachment(AcDbMText::kBottomLeft);
	return CCreateEnt::PostToModelSpace(pMText); 
}