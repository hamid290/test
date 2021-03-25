package uk.co.hamid.wiprotest.network

import retrofit2.Response
import uk.co.hamid.wiprotest.network.model.PlantDataApiService
import uk.co.hamid.wiprotest.network.model.PlantsResponse
import javax.inject.Inject

class PlantDataApiHelperImpl @Inject constructor(
    private val plantDataApiService: PlantDataApiService
): PlantDataApiHelper {
    override suspend fun getPlants(): Response<PlantsResponse> = plantDataApiService.getPlants()
}