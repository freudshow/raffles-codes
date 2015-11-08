#include "StdAfx.h"
#include "ModifyEnt.h"

CModifyEnt::CModifyEnt(void)
{
}

CModifyEnt::~CModifyEnt(void)
{
}

Acad::ErrorStatus CModifyEnt::ChangeColor(AcDbObjectId entId, Adesk::UInt16 colorIndex)
{ 
	AcDbEntity *pEntity;
	acdbOpenObject(pEntity, entId, AcDb::kForWrite); 
	pEntity->setColorIndex(colorIndex);
	pEntity->close();
	return Acad::eOk;
}

Acad::ErrorStatus CModifyEnt::ChangeLayer(AcDbObjectId entId, CString strLayerName)
{
	AcDbEntity *pEntity;
	acdbOpenObject(pEntity, entId, AcDb::kForWrite);
	pEntity->setLayer(strLayerName);
	pEntity->close();
	return Acad::eOk;
} 

Acad::ErrorStatus CModifyEnt::ChangeLinetype(AcDbObjectId entId, CString strLinetype)
{
	AcDbEntity *pEntity;
	acdbOpenObject(pEntity, entId, AcDb::kForWrite);
	pEntity->setLayer(strLinetype);
	pEntity->close();
	return Acad::eOk;
}

Acad::ErrorStatus CModifyEnt::Rotate(AcDbObjectId entId, AcGePoint2d ptBase, double rotation) 
{
	AcGeMatrix3d xform;
	AcGeVector3d vec(0, 0, 1);
	xform.setToRotation(rotation, vec, CCalculation::Pt2dTo3d(ptBase));
	AcDbEntity *pEnt;
	Acad::ErrorStatus es = acdbOpenObject(pEnt, entId, AcDb::kForWrite, false);
	pEnt->transformBy(xform);
	pEnt->close();
	return es;
}

Acad::ErrorStatus CModifyEnt::Move(AcDbObjectId entId, AcGePoint3d ptBase, AcGePoint3d ptDest)
{
	AcGeMatrix3d xform;
	AcGeVector3d vec(ptDest.x - ptBase.x, ptDest.y - ptBase.y, ptDest.z - ptBase.z);
	xform.setToTranslation(vec);
	AcDbEntity *pEnt;
	Acad::ErrorStatus es = acdbOpenObject(pEnt, entId, AcDb::kForWrite, false);
	pEnt->transformBy(xform);
	pEnt->close();
	return es;
}

Acad::ErrorStatus CModifyEnt::Scale(AcDbObjectId entId, AcGePoint3d ptBase, double scaleFactor)
{
	AcGeMatrix3d xform;
	xform.setToScaling(scaleFactor, ptBase);
	AcDbEntity *pEnt;
	Acad::ErrorStatus es = acdbOpenObject(pEnt, entId, AcDb::kForWrite, false);
	pEnt->transformBy(xform);
	pEnt->close();
	return es;
}