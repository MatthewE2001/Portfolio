/*
Author: Matthew Esslie
Class: GPR 480-01
Assignment: Assignment 7
Certification of Authenticity:
I certify that this assignment is entirely my own work.
 */
#include "PathingPool.h"
#include "AStarPathfinder.h"

PathingPool::PathingPool()
{
	mNumberOfThreads = 1; //minimum value of 1
	mCompletedThreadsPerSecond = 0;
}

PathingPool::~PathingPool()
{
	//these are unsigned to remove some warnings
	for (unsigned int i = 0; i < mThreads.size(); i++)
	{
		mThreads.pop_front();
	}

	for (unsigned int i = 0; i < mPathingRequests.size(); i++)
	{
		mPathingRequests.pop();
	}

	for (unsigned int i = 0; i < mPaths.size(); i++)
	{
		mPaths.pop_front();
	}
}

void PathingPool::changeNumberOfThreads(int threadVal)
{
	mNumberOfThreads = threadVal;
}

void PathingPool::resetCompletedThreadCount()
{
	mCompletedThreadsPerSecond = 0;
}

int PathingPool::getNumberOfThreads()
{
	return mThreads.size();
}

int PathingPool::getCompletedThreadCount()
{
	return mCompletedThreadsPerSecond;
}

void PathingPool::increaseThreadCount(PathingRequests pathRequest)
{
	mNumberOfThreads++;
	Path newPath = findPath(pathRequest.startNode, pathRequest.endNode);

	addPathToList(newPath);
	mThreads.push_back(std::thread(findPath, pathRequest.startNode, pathRequest.endNode));
}

void PathingPool::decreaseThreadCount()
{
	if (mNumberOfThreads > 1)
	{
		mNumberOfThreads--;
		
		mThreads.front().join();
		mThreads.pop_front();
	}
	else
	{
		return;
	}
}

PathingRequests PathingPool::getPathingRequest()
{
	PathingRequests tmp = mPathingRequests.front();
	mPathingRequests.pop();

	return tmp;
}

void PathingPool::addPathingRequest(PathingRequests newPath)
{
	mPathingRequests.push(newPath);
}

void PathingPool::removePathingRequest()
{
	mPathingRequests.pop();
}

void PathingPool::addPathToList(Path pathToAdd)
{
	mPaths.push_back(pathToAdd);
}

Path PathingPool::getPathFromList(int index)
{
	return Path();
}

void PathingPool::updateThreadsAndPaths()
{
	std::list<std::thread>::iterator iter;

	for (iter = mThreads.begin(); iter != mThreads.end(); iter++)
	{
		if (iter->joinable())
		{
			//mThreads.erase(iter);
			iter->join();
			//iter->detach();

			mCompletedThreadsPerSecond++;
		}

		mNumberOfThreads--;
	}

	for (unsigned int i = 0; i < mPathingRequests.size(); i++)
	{
		mPathingRequests.pop();
	}
}
