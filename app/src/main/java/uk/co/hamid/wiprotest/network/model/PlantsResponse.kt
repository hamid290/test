package uk.co.hamid.wiprotest.network.model

import com.google.gson.annotations.Expose
import com.google.gson.annotations.SerializedName

class PlantsResponse {
    @SerializedName("data")
    @Expose
    var plantlist: MutableList<Plant>? = null

    // For simplicity, We don't care about links and meta in this app
    /*
    @SerializedName("links")
    @Expose
    var links: Int? = null

    @SerializedName("meta")
    @Expose
    var meta: String? = null
    */
}
