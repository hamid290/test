package uk.co.hamid.wiprotest.network.model

import retrofit2.Response
import retrofit2.http.GET

interface PlantDataApiService{
    /**
     * Get the list of the Plants from the API
     */
    @GET("/api/v1/plants")
    suspend fun getPlants(): Response<PlantsResponse>

}
