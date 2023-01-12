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
}
