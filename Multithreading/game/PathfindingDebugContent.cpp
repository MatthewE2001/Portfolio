#include "PathfindingDebugContent.h"
#include "GridPathfinder.h"
#include "Path.h"
#include "GameApp.h"

#include <sstream>

using namespace std;

PathfindingDebugContent::PathfindingDebugContent()
{
}

string PathfindingDebugContent::getDebugString()
{
	stringstream theStream;

	GameApp* pGameApp = dynamic_cast<GameApp*>(gpGame);
	GridPathfinder* pathfinder = pGameApp->getPathfinder();

#ifdef VISUALIZE_PATH
	if(pathfinder->mPath.getNumNodes() > 0 )
	{
		theStream << "Pathlength:"<< pathfinder->mPath.getNumNodes();
	}
	
	theStream << "  Num Nodes Processed:" << pathfinder->mVisitedNodes.size();
#endif
	theStream << "  Elapsed Time:" << pathfinder->mTimeElapsed;
	return theStream.str();
}

