package uk.co.hamid.wiprotest

import dagger.hilt.android.testing.HiltAndroidRule
import dagger.hilt.android.testing.HiltAndroidTest
import io.mockk.coEvery
import io.mockk.coVerify
import io.mockk.mockk
import okhttp3.ResponseBody
import org.junit.Assert.assertEquals
import org.junit.Before
import org.junit.Rule
import org.junit.Test
import retrofit2.Response
import uk.co.hamid.wiprotest.network.NetworkStatus
import uk.co.hamid.wiprotest.network.PlantDataApiHelper
import uk.co.hamid.wiprotest.network.Resource
import uk.co.hamid.wiprotest.network.model.Plant
import uk.co.hamid.wiprotest.network.model.PlantsResponse
import uk.co.hamid.wiprotest.repository.PlantRepository
import uk.co.hamid.wiprotest.viewmodel.ActivityViewModel
import java.net.HttpURLConnection

@HiltAndroidTest
class ViewModelInstrumentedTests {

    // Caveat: We need the hiltRule to always run first before any other rules (e.g. before Activity scenario rules)
    // So we specify the order = 0 rule parameter (which is available in Junit 4.13 dep or higher)
    // if we have multiple rules:
    //@get:Rule(order = 0)
    @get:Rule()
    var hiltRule = HiltAndroidRule(this)

    lateinit var plantApiHelper: PlantDataApiHelper

    lateinit var plantRepository : PlantRepository

    lateinit var viewModel: ActivityViewModel

    @Before
    fun init(){
        hiltRule.inject()

        plantApiHelper = mockk<PlantDataApiHelper>(relaxed = true)
        plantRepository = PlantRepository(plantApiHelper)
        viewModel = ActivityViewModel(plantRepository)
    }

    private fun getMeFakePlant(): Plant {

        return Plant(
            id = "678281",
            commonName = "Evergreen oak",
            slug = "quercus-rotundifolia",
            scientificName = "Quercus rotundifolia",
            year = "1785",
            bibliography = "Encycl. 1: 723 (1785)",
            author = "Lam.",
            status = "accepted",
            rank = "species",
            familyCommonName = "Beech family",
            genusId = 5778,
            imageUrl = "https://bs.plantnet.org/image/o/1a03948baf0300da25558c2448f086d39b41ca30",
            genus = "Quercus",
            family = "Fagaceae"
        )
    }

    @Test
    fun viewModelReturnsAPlantWhenApiSuccessfullyReturnsAPlantTest() {

        //Given

        // Make our Api returns a faked plant response:
        val expectedPlant = getMeFakePlant()
        val expectedPlantResponse = PlantsResponse()
        expectedPlantResponse.plantlist = mutableListOf<Plant>(expectedPlant)

        coEvery { plantApiHelper.getPlants() } returns Response.success(expectedPlantResponse)

        //When
        viewModel.getPlants()

        //Then

        // Verify that view model call the repository to pull from api:
        coVerify { plantRepository.getPlants() }

        // And finally the view model gives out the plant resource we expected:
        val expectedResource = Resource(
            data = expectedPlantResponse,
            status = NetworkStatus.SUCCESS,
            message = null
        )
        assertEquals(expectedResource, viewModel.res.value)
    }

    @Test
    fun viewModelReturnsErrorWhenApiFailsTest() {

        //Given
        // API returns an error (e.g. 500 internal server error)
        val errorResponseBody = ResponseBody.create(null, "500 Internal server error")

        coEvery { plantApiHelper.getPlants() } returns Response.error(HttpURLConnection.HTTP_INTERNAL_ERROR, errorResponseBody)

        //When
        viewModel.getPlants()

        //Then
        coVerify { plantRepository.getPlants() }

        val errorResource = Resource.error(errorResponseBody.toString(), null)

        assertEquals(errorResource, viewModel.res.value)
    }
}
