void PrintFloat2(float2 vec, __constant char * name)
{
	printf("%s", name);
	printf(" - %.10f, %.10f\n", vec.x, vec.y);
}


__kernel void CalculateGravity(__constant float * pos1, __constant float * pos2,
	__global float * out_dir1, __global float * out_dir2,
	float mass1, float mass2,
	float simSpeed, float elapsedTime) 
{
	float2 pos1Vec;
	pos1Vec.x = pos1[0];
	pos1Vec.y = pos1[1];
	
	float2 pos2Vec;
	pos2Vec.x = pos2[0];
	pos2Vec.y = pos2[1];
	
	float2 dir1Vec;
	dir1Vec.x = out_dir1[0];
	dir1Vec.y = out_dir1[1];
	
	float2 dir2Vec;
	dir2Vec.x = out_dir2[0];
	dir2Vec.y = out_dir2[1];

	/*PrintFloat2(pos1Vec, "pos1");
	PrintFloat2(pos2Vec, "pos2");
	PrintFloat2(dir1Vec, "dir1");
	PrintFloat2(dir2Vec, "dir2");
	printf("mass1 - %f \n", mass1);
	printf("mass2 - %f \n", mass2);
	printf("simSpeed - %f \n", simSpeed);
	printf("elapsedTime - %f \n", elapsedTime);*/

	float2 directionVec = pos2Vec - pos1Vec;
	
	//PrintFloat2(directionVec, "distanceVec");
	
	float dis = length(directionVec);
	if(!dis)
		return;
		
	//printf("distance - %f \n", dis);
		
	dis = dis * dis;
	
	//printf("disSquared - %f \n", dis);
	
	float acceleration = 0.0000000000000000667408F * simSpeed / dis * elapsedTime;
	
	//printf("acceleration - %.10f \n", acceleration);
	
	directionVec = normalize(directionVec) * acceleration;
	
	//PrintFloat2(directionVec, "normalized accelerated direction");
	
	dir1Vec += (directionVec * mass1);
	dir2Vec -= (directionVec * mass2);
	
	//PrintFloat2(dir1Vec, "result dir1");
	//PrintFloat2(dir2Vec, "result dir2");
	
	out_dir1[0]= dir1Vec.x;
	out_dir1[1]= dir1Vec.y;
	out_dir2[0]= dir2Vec.x;
	out_dir2[1]= dir2Vec.y;
}