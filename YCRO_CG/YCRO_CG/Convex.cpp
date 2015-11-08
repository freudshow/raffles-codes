#include "StdAfx.h"
#include "Convex.h"
#include "StdAfx.h"
#include "resource.h"
#include <tchar.h>
#include <iostream>
#include "CreateEnt.h"
#include "ModifyEnt.h"
#include "Calculation.h"

using namespace std;

Convex::Convex(void)
{
}

Convex::~Convex(void)
{
}

static int ccw(const AcGePoint3d &p1,const AcGePoint3d &p2, const AcGePoint3d &p3)
{
	double or = (p2.x - p1.x)*(p3.y - p1.y)-(p2.y - p1.y)*(p3.x - p1.x);
	if(or < 0.0)
		return -1;
	else if(or > 0.0)
		return 1;
	else return 0;
}

static bool PointSortByX(const AcGePoint3d &a, const AcGePoint3d &b)
{
	if(a.x == b.x)
		return a.y < b.y;
	return a.x < b.x;
	std::sinh()
}

static void getPoints(PointVector &vec)
{
	AcDbObjectIdArray ids;
	CResbufPtr pFilter (acutBuildList(RTDXF0,ACRX_T("POINT,LINE,LWPOLYLINE"),NULL));
	CSelectionSet sset;
	sset.userSelect(pFilter);
	sset.asArray(ids);
	for(int i = 0 ; i < ids.length(); i++)
	{
		AcDbEntityPointer pEnt(ids[i],AcDb::kForRead);
		if(pEnt.openStatus() == eOk )
		{
			if(pEnt->isA() == AcDbPoint::desc())
			{
				AcDbPoint *pPoint = AcDbPoint::cast(pEnt);
				if(pPoint)
				{
					vec.push_back(pPoint->position());
				}
			}
			else if(pEnt->isA() == AcDbLine::desc())
			{
				AcDbLine *pLine = AcDbLine::cast(pEnt);
				if(pLine)
				{
					vec.push_back(pLine->startPoint());
					vec.push_back(pLine->endPoint());
				}
			}
			else if(pEnt->isA() == AcDbPolyline::desc())
			{
				AcDbPolyline *pLine = AcDbPolyline::cast(pEnt);
				if(pLine)
				{
					for(int i = 0 ; i < pLine->numVerts() ; i++)
					{
						AcGePoint3d pt;
						pLine->getPointAt(i,pt);
						vec.push_back(pt);
					}
				}
			}
		}
	}
}

static void sortPoints(PointVector &points, PointVector &ptsout)
{
	PointVector upper;
	PointVector lower;
	AcGePoint3d left, right;
	PointVector::const_iterator iter;
	PointVector::const_reverse_iterator riter;

	std::sort(points.begin(), points.end(), PointSortByX);

	if(points.size() > 3)
	{
		left = points.front();
		right = points.back();

		for ( size_t i = 0 ; i < points.size() ; i++ )
		{
			int dir = ccw( left, right, points[ i ] );
			if ( dir < 0 )
				upper.push_back( points[ i ] );
			else
				lower.push_back( points[ i ] );
		}

		for ( iter = upper.begin() ; iter != upper.end() ; ++iter )
			ptsout.push_back(AcGePoint3d((*iter).x, (*iter).y , (*iter).z));

		for ( riter = lower.rbegin() ; riter != lower.rend() ; ++riter )
			ptsout.push_back(AcGePoint3d((*riter).x, (*riter).y , (*riter).z));
	}
}

static void grahamScan(const PointVector &points , PointVector &ptsout)
{
	PointDeque pntQue;
	PointDeque::const_iterator iter;
	pntQue.push_front(points[0]);
	pntQue.push_front(points[1]);

	unsigned int i = 2;

	while(i < points.size())
	{
		if (pntQue.size() > 1)
		{
			if (ccw(pntQue[1],pntQue[0],points[i]) == 1)
				pntQue.push_front(points[i++]);
			else
				pntQue.pop_front();
		}
		else
			pntQue.push_front(points[i++]);
	}

	for(iter = pntQue.begin(); iter != pntQue.end(); ++iter)
		ptsout.push_back(AcGePoint3d((*iter).x, (*iter).y , (*iter).z));

}

// - ArxConvexHull._doit command (do not rename)
static void ArxConvexHull_doit(void)
{
	PointVector::const_iterator iter;
	PointVector basepoints,sortedPoints;
	getPoints(basepoints);
	sortPoints(basepoints,sortedPoints);
	basepoints.clear();
	grahamScan(sortedPoints,basepoints);
	AcDbPolyline *pline = new AcDbPolyline(basepoints.size());

	for(size_t i = 0 ; i < basepoints.size(); i++)
		pline->addVertexAt(i,AcGePoint2d(basepoints[i].x,basepoints[i].y));

	pline->setClosed(Adesk::kTrue);
	AddEntityToDataBase(pline);
}

static void AddEntityToDataBase(AcDbEntity *pEnt) 
{
	AcDbDatabase* pDb = acdbHostApplicationServices()->workingDatabase();
	AcDbBlockTableRecordPointer pBTR(pDb->currentSpaceId(), AcDb::kForWrite); 
	if (pEnt && Acad::eOk == pBTR.openStatus())
	{
		pBTR->appendAcDbEntity(pEnt);
		pEnt->close();
	}
}
