/*
Author: Matthew Esslie
Class: GPR 480-01
Assignment: Assignment 7
Certification of Authenticity:
I certify that this assignment is entirely my own work.

Specifically find path function
 */
#pragma once

#include "GridPathfinder.h"
#include "NodeRecord.h"
#include <vector>


class Path;
class Graph;
class Grid;

Path findPath(Node* pFrom, Node* pTo);

class AStarPathfinder :public GridPathfinder
{
public:
	AStarPathfinder(Graph* pGraph);
	~AStarPathfinder();

	virtual Path findPath(Node* pFrom, Node* pTo);

protected:
	float getHeuristicCost(Node* pFrom, Node* pTo);
};
