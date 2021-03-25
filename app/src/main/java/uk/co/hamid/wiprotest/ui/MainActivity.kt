package uk.co.hamid.wiprotest.ui

import android.os.Bundle
import android.view.View
import androidx.activity.viewModels
import androidx.appcompat.app.AppCompatActivity
import androidx.lifecycle.Observer
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.DividerItemDecoration
import com.google.android.material.snackbar.Snackbar
import dagger.hilt.android.AndroidEntryPoint
import uk.co.hamid.wiprotest.R
import uk.co.hamid.wiprotest.databinding.ActivityPlantDataBinding
import uk.co.hamid.wiprotest.network.NetworkStatus
import uk.co.hamid.wiprotest.viewmodel.ActivityViewModel
import javax.inject.Inject

@AndroidEntryPoint
class MainActivity : AppCompatActivity() {

    // Here we make use of the Activity KTX for easy viewModel retrieval:
    private val plantDataActivityViewModel: ActivityViewModel by viewModels()

    @Inject
    lateinit var networkDataAdapter: Adapter

    private lateinit var binding: ActivityPlantDataBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ActivityPlantDataBinding.inflate(layoutInflater)
        val view = binding.root
        setContentView(view)

        prepareView()
        callApiAndUpdateUI()
    }

    private fun prepareView() {
        binding.plantsRecycleView.itemAnimator = DefaultItemAnimator()
        binding.plantsRecycleView.addItemDecoration(
            DividerItemDecoration(
                this,
                DividerItemDecoration.VERTICAL
            )
        )
        binding.plantsRecycleView.adapter = networkDataAdapter
        networkDataAdapter.onItemClick = { user ->

            // Show the plant details in a popup Bottom sheet:
            val bottomSheetFragment =
                DetailFragment(user)
            bottomSheetFragment.show(supportFragmentManager, bottomSheetFragment.tag)
        }

    }

    private fun callApiAndUpdateUI() {
        binding.loadingProgressBar.visibility = View.VISIBLE

        plantDataActivityViewModel.res.observe(this, Observer {
            when(it.status){
                NetworkStatus.SUCCESS -> {
                    binding.loadingProgressBar.visibility = View.GONE
                    binding.plantsRecycleView.visibility = View.VISIBLE

                    it.data.let {res->
                        res?.plantlist?.let { plants -> networkDataAdapter.addItemList(plants) }
                    }

                }
                NetworkStatus.LOADING -> {
                    binding.loadingProgressBar.visibility = View.VISIBLE
                    binding.plantsRecycleView.visibility = View.GONE
                }
                NetworkStatus.ERROR -> {
                    binding.loadingProgressBar.visibility = View.GONE
                    binding.plantsRecycleView.visibility = View.VISIBLE
                    showSnackBar(getString(R.string.msg_something_went_wrong))
                }
            }
        })

        plantDataActivityViewModel.getPlants()
    }

    private fun showSnackBar(msg: String) {
        Snackbar.make(findViewById(android.R.id.content), msg, Snackbar.LENGTH_SHORT)
            .show()
    }

}