/*
Author: Matthew Esslie
Class: GPR 480-01
Assignment: Assignment 7
Certification of Authenticity:
I certify that this assignment is entirely my own work.
 */
#pragma once

#include <queue>
#include <thread>
#include <list>
#include "Node.h"
#include "Graph.h"
#include "AStarPathfinder.h"

struct PathingRequests
{
	Node* startNode;
	Node* endNode;
};

//use A* pathfinding (should already be happening but just a note)
class PathingPool : public Trackable
{
	private:
		std::queue<PathingRequests> mPathingRequests;
		std::list<std::thread> mThreads; 
		std::list<Path> mPaths;

		int mNumberOfThreads;
		int mCompletedThreadsPerSecond;
	public:
		PathingPool();
		~PathingPool();

		void changeNumberOfThreads(int threadVal);
		void resetCompletedThreadCount();
		int getNumberOfThreads();
		int getCompletedThreadCount();

		void increaseThreadCount(PathingRequests pathRequest);
		void decreaseThreadCount();

		PathingRequests getPathingRequest();
		void addPathingRequest(PathingRequests newPath);
		void removePathingRequest();

		void addPathToList(Path pathToAdd);
		Path getPathFromList(int index); //do I want an index or something else

		void updateThreadsAndPaths();
};