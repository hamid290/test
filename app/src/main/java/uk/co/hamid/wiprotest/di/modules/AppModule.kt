package uk.co.hamid.wiprotest.di.modules

import com.google.gson.Gson
import com.google.gson.GsonBuilder
import dagger.Module
import dagger.Provides
import dagger.hilt.InstallIn
import dagger.hilt.components.SingletonComponent
import okhttp3.OkHttpClient
import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory
import uk.co.hamid.wiprotest.BuildConfig
import uk.co.hamid.wiprotest.network.PlantDataApiHelper
import uk.co.hamid.wiprotest.network.PlantDataApiHelperImpl
import uk.co.hamid.wiprotest.network.model.PlantDataApiService
import javax.inject.Singleton

@Module
@InstallIn(SingletonComponent::class)
object AppModule {

    @Provides
    fun baseUrl(): String {
        return "https://trefle.io"
    }

    @Provides
    fun provideGson(): Gson = GsonBuilder().create()

    @Provides
    fun provideHttpClient(): OkHttpClient = OkHttpClient.Builder()
        // add token as a parameter to every request
        .addInterceptor { chain ->
            // Pull out of local.properties file so not in Version Control
            val apiToken = BuildConfig.API_TOKEN

            val url = chain
                .request()
                .url()
                .newBuilder()
                .addQueryParameter("token", apiToken)
                .build()
            chain.proceed(chain.request().newBuilder().url(url).build())
        }
        .build()

    @Provides
    fun provideRetrofit(client: OkHttpClient, gson: Gson, baseUrl: String) = Retrofit.Builder()
        .client(client)
        .baseUrl(baseUrl)
        .addConverterFactory(GsonConverterFactory.create(gson))
        .build()

    @Provides
    @Singleton
    fun providePlantDataApiService(retrofit: Retrofit) = retrofit.create(PlantDataApiService::class.java)

    @Provides
    @Singleton
    fun providePlantDataApiHelper(apiHelper: PlantDataApiHelperImpl): PlantDataApiHelper = apiHelper

}