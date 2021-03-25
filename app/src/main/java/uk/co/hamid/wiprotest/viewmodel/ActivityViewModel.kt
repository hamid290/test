package uk.co.hamid.wiprotest.viewmodel

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.launch
import uk.co.hamid.wiprotest.network.Resource
import uk.co.hamid.wiprotest.network.model.PlantsResponse
import uk.co.hamid.wiprotest.repository.PlantRepository
import javax.inject.Inject

@HiltViewModel
class ActivityViewModel @Inject constructor(
    private val plantRepository: PlantRepository
) : ViewModel() {

    private val _res = MutableLiveData<Resource<PlantsResponse>>()

    val res : LiveData<Resource<PlantsResponse>>
        get() = _res

    init {
        //getPlants()
    }

    fun getPlants()  = viewModelScope.launch {
        _res.postValue(Resource.loading(null))

        plantRepository.getPlants().let {
            if (it.isSuccessful){
                _res.postValue(Resource.success(it.body()))
            }else{
                _res.postValue(Resource.error(it.errorBody().toString(), null))
            }
        }
    }

}