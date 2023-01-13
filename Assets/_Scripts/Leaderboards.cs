using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;
using SA.iOS.GameKit;

public class Leaderboards : TheYeti
{
    private void Start()
    {
        StartCoroutine(AuthenticatePlayer());
    }

    IEnumerator AuthenticatePlayer()
    {
        bool done = false;

        ISN_GKLocalPlayer.SetAuthenticateHandler(result => {
            if (result.IsSucceeded)
            {
                Debug.Log("Authenticate is succeeded!");
                var player = ISN_GKLocalPlayer.LocalPlayer;
                Debug.Log($"player id: {player.PlayerID}");
                Debug.Log($"player Alias: {player.Alias}");
                Debug.Log($"player DisplayName: {player.DisplayName}");
                Debug.Log($"player Authenticated: {player.Authenticated}");
                Debug.Log($"player Underage: {player.Underage}");
                player.GenerateIdentityVerificationSignatureWithCompletionHandler(signatureResult => {
                    if (signatureResult.IsSucceeded)
                    {
                        Debug.Log($"signatureResult.PublicKeyUrl: {signatureResult.PublicKeyUrl}");
                        Debug.Log($"signatureResult.Timestamp: {signatureResult.Timestamp}");
                        Debug.Log($"signatureResult.Salt.Length: {signatureResult.Salt.Length}");
                        Debug.Log($"signatureResult.Signature.Length: {signatureResult.Signature.Length}");
                    }
                    else
                    {
                        Debug.LogError($"IdentityVerificationSignature has failed: {signatureResult.Error.FullMessage}");
                    }
                });
                done = true;
            }
            else
            {
                Debug.LogError($"Authenticate is failed! Error with code: {result.Error.Code} and description: {result.Error.Message}");
                done = true;
            }
        });

        yield return new WaitWhile(() => done == false);
    }

    public void ShowLeaderboards()
    {
        ISN_GKGameCenterViewController viewController = new ISN_GKGameCenterViewController();
        viewController.ViewState = ISN_GKGameCenterViewControllerState.Leaderboards;
        viewController.Show();
    }

    public void SendScores(int high, int kills)
    {
        ISN_GKScore scoreReporter1 = new ISN_GKScore("bestscore");
        scoreReporter1.Value = high;
        scoreReporter1.Context = 1;

        ISN_GKScore scoreReporter2 = new ISN_GKScore("totalkills");
        scoreReporter2.Value = kills;
        scoreReporter2.Context = 1;

        var scores = new List<ISN_GKScore>() { scoreReporter1, scoreReporter2 };

        ISN_GKScore.ReportScores(scores, (result) => {
            if (result.IsSucceeded)
            {
                Debug.Log("Score Report Success");
            }
            else
            {
                Debug.Log("Score Report failed! Code: " + result.Error.Code + " Message: " + result.Error.Message);
            }
        });
    }
}
