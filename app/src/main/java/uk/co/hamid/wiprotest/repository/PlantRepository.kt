package uk.co.hamid.wiprotest.repository

import uk.co.hamid.wiprotest.network.PlantDataApiHelper
import javax.inject.Inject

class PlantRepository @Inject constructor(
    private val apiHelper: PlantDataApiHelper
){
    suspend fun getPlants() = apiHelper.getPlants()
}