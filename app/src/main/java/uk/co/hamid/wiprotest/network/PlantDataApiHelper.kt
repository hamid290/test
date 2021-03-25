package uk.co.hamid.wiprotest.network

import retrofit2.Response
import uk.co.hamid.wiprotest.network.model.PlantsResponse

interface PlantDataApiHelper {
    suspend fun getPlants(): Response<PlantsResponse>
}